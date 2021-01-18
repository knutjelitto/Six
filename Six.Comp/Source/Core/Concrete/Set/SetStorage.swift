//===----------------------------------------------------------------------===//
//
// This source file is part of the Swift.org open source project
//
// Copyright (c) 2014 - 2018 Apple Inc. and the Swift project authors
// Licensed under Apache License v2.0 with Runtime Library Exception
//
// See https://swift.org/LICENSE.txt for license information
// See https://swift.org/CONTRIBUTORS.txt for the list of Swift project authors
//
//===----------------------------------------------------------------------===//

import SwiftShims

/// An instance of this class has all `Set` data tail-allocated.
/// Enough bytes are allocated to hold the bitmap for marking valid entries,
/// keys, and values. The data layout starts with the bitmap, followed by the
/// keys, followed by the values.
// NOTE: older runtimes called this class _RawSetStorage. The two
// must coexist without a conflicting ObjC class name, so it was
// renamed. The old name must not be used in the new runtime.
internal class __RawSetStorage: __SwiftNativeNSSet {
  // NOTE: The precise layout of this type is relied on in the runtime to
  // provide a statically allocated empty singleton.  See
  // stdlib/public/stubs/GlobalObjects.cpp for details.

  /// The current number of occupied entries in this set.
  internal final var _count: Int

  /// The maximum number of elements that can be inserted into this set without
  /// exceeding the hash table's maximum load factor.
  internal final var _capacity: Int

  /// The scale of this set. The number of buckets is 2 raised to the
  /// power of `scale`.
  internal final var _scale: Int8

  /// The scale corresponding to the highest `reserveCapacity(_:)` call so far,
  /// or 0 if there were none. This may be used later to allow removals to
  /// resize storage.
  ///
  /// FIXME: <rdar://problem/18114559> Shrink storage on deletion
  internal final var _reservedScale: Int8

  // Currently unused, set to zero.
  internal final var _extra: Int16

  /// A mutation count, enabling stricter index validation.
  internal final var _age: Int32

  /// The hash seed used to hash elements in this set instance.
  internal final var _seed: Int

  /// A raw pointer to the start of the tail-allocated hash buffer holding set
  /// members.
  internal final var _rawElements: UnsafeMutableRawPointer

  // This type is made with allocWithTailElems, so no init is ever called.
  // But we still need to have an init to satisfy the compiler.
  internal init(_doNotCallMe: ()) {
    _internalInvariantFailure("This class cannot be directly initialized")
  }

  internal final var _bucketCount: Int {
    get { return 1 &<< _scale }
  }

  internal final var _metadata: UnsafeMutablePointer<_HashTable.Word> {
    get {
      let address = Builtin.projectTailElems(self, _HashTable.Word.self)
      return UnsafeMutablePointer(address)
    }
  }

  // The _HashTable struct contains pointers into tail-allocated storage, so
  // this is unsafe and needs `_fixLifetime` calls in the caller.
  internal final var _hashTable: _HashTable {
    get {
      return _HashTable(words: _metadata, bucketCount: _bucketCount)
    }
  }
}

/// The storage class for the singleton empty set.
/// The single instance of this class is created by the runtime.
// NOTE: older runtimes called this class _EmptySetSingleton. The two
// must coexist without conflicting ObjC class names, so it was renamed.
// The old names must not be used in the new runtime.
internal class __EmptySetSingleton: __RawSetStorage {
  override internal init(_doNotCallMe: ()) {
    _internalInvariantFailure("This class cannot be directly initialized")
  }
}

extension __RawSetStorage {
  /// The empty singleton that is used for every single Set that is created
  /// without any elements. The contents of the storage must never be mutated.
  internal static var empty: __EmptySetSingleton {
    return Builtin.bridgeFromRawPointer(
      Builtin.addressof(&_swiftEmptySetSingleton))
  }
}

extension __EmptySetSingleton: _NSSetCore {
}

final internal class _SetStorage<Element: Hashable>
  : __RawSetStorage, _NSSetCore {
  // This type is made with allocWithTailElems, so no init is ever called.
  // But we still need to have an init to satisfy the compiler.
  override internal init(_doNotCallMe: ()) {
    _internalInvariantFailure("This class cannot be directly initialized")
  }

  deinit {
    guard _count > 0 else { return }
    if !_isPOD(Element.self) {
      let elements = _elements
      for bucket in _hashTable {
        (elements + bucket.offset).deinitialize(count: 1)
      }
    }
    _fixLifetime(self)
  }

  final internal var _elements: UnsafeMutablePointer<Element> {
    get {
      return self._rawElements.assumingMemoryBound(to: Element.self)
    }
  }

  internal var asNative: _NativeSet<Element> {
    return _NativeSet(self)
  }

}

extension _SetStorage {
  internal static func copy(original: __RawSetStorage) -> _SetStorage {
    return .allocate(
      scale: original._scale,
      age: original._age,
      seed: original._seed)
  }

  static internal func resize(
    original: __RawSetStorage,
    capacity: Int,
    move: Bool
  ) -> _SetStorage {
    let scale = _HashTable.scale(forCapacity: capacity)
    return allocate(scale: scale, age: nil, seed: nil)
  }

  static internal func allocate(capacity: Int) -> _SetStorage {
    let scale = _HashTable.scale(forCapacity: capacity)
    return allocate(scale: scale, age: nil, seed: nil)
  }

  static internal func allocate(
    scale: Int8,
    age: Int32?,
    seed: Int?
  ) -> _SetStorage {
    // The entry count must be representable by an Int value; hence the scale's
    // peculiar upper bound.
    _internalInvariant(scale >= 0 && scale < Int.bitWidth - 1)

    let bucketCount = (1 as Int) &<< scale
    let wordCount = _UnsafeBitset.wordCount(forCapacity: bucketCount)
    let storage = Builtin.allocWithTailElems_2(
      _SetStorage<Element>.self,
      wordCount._builtinWordValue, _HashTable.Word.self,
      bucketCount._builtinWordValue, Element.self)

    let metadataAddr = Builtin.projectTailElems(storage, _HashTable.Word.self)
    let elementsAddr = Builtin.getTailAddr_Word(
      metadataAddr, wordCount._builtinWordValue, _HashTable.Word.self,
      Element.self)
    storage._count = 0
    storage._capacity = _HashTable.capacity(forScale: scale)
    storage._scale = scale
    storage._reservedScale = 0
    storage._extra = 0

    if let age = age {
      storage._age = age
    } else {
      // The default mutation count is simply a scrambled version of the storage
      // address.
      storage._age = Int32(
        truncatingIfNeeded: ObjectIdentifier(storage).hashValue)
    }

    storage._seed = seed ?? _HashTable.hashSeed(for: storage, scale: scale)
    storage._rawElements = UnsafeMutableRawPointer(elementsAddr)

    // Initialize hash table metadata.
    storage._hashTable.clear()
    return storage
  }
}
