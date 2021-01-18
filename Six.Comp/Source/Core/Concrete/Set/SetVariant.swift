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

/// This protocol is only used for compile-time checks that
/// every buffer type implements all required operations.
internal protocol _SetBuffer {
  associatedtype Element
  associatedtype Index

  var startIndex: Index { get }
  var endIndex: Index { get }
  func index(after i: Index) -> Index
  func index(for element: Element) -> Index?
  var count: Int { get }

  func contains(_ member: Element) -> Bool
  func element(at i: Index) -> Element
}

extension Set {
  internal struct _Variant {
    internal var object: _BridgeStorage<__RawSetStorage>

    init(dummy: ()) {
      self.object = _BridgeStorage(taggedPayload: 0)
    }

    init(native: __owned _NativeSet<Element>) {
      self.object = _BridgeStorage(native: native._storage)
    }
  }
}

extension Set._Variant {

  internal mutating func isUniquelyReferenced() -> Bool {
    return object.isUniquelyReferencedUnflaggedNative()
  }

  internal var asNative: _NativeSet<Element> {
    get {
      return _NativeSet(object.unflaggedNativeInstance)
    }
    set {
      self = .init(native: newValue)
    }
    _modify {
      var native = _NativeSet<Element>(object.unflaggedNativeInstance)
      self = .init(dummy: ())
      defer {
        // This is in a defer block because yield might throw, and we need to
        // preserve Set's storage invariants when that happens.
        object = .init(native: native._storage)
      }
      yield &native
    }
  }

  /// Reserves enough space for the specified number of elements to be stored
  /// without reallocating additional storage.
  internal mutating func reserveCapacity(_ capacity: Int) {
    let isUnique = isUniquelyReferenced()
    asNative.reserveCapacity(capacity, isUnique: isUnique)
  }

  /// The number of elements that can be stored without expanding the current
  /// storage.
  ///
  /// For bridged storage, this is equal to the current count of the
  /// collection, since any addition will trigger a copy of the elements into
  /// newly allocated storage. For native storage, this is the element count
  /// at which adding any more elements will exceed the load factor.
  internal var capacity: Int {
    return asNative.capacity
  }
}

extension Set._Variant: _SetBuffer {
  internal typealias Index = Set<Element>.Index

  internal var startIndex: Index {
    return asNative.startIndex
  }

  internal var endIndex: Index {
    return asNative.endIndex
  }

  internal func index(after index: Index) -> Index {
    return asNative.index(after: index)
  }

  internal func formIndex(after index: inout Index) {
    index = asNative.index(after: index)
  }

  internal func index(for element: Element) -> Index? {
    return asNative.index(for: element)
  }

  internal var count: Int {
    get {
      return asNative.count
    }
  }

  internal func contains(_ member: Element) -> Bool {
    return asNative.contains(member)
  }

  internal func element(at index: Index) -> Element {
    return asNative.element(at: index)
  }
}

extension Set._Variant {
  internal mutating func update(with value: __owned Element) -> Element? {
    let isUnique = self.isUniquelyReferenced()
    return asNative.update(with: value, isUnique: isUnique)
  }

  internal mutating func insert(
    _ element: __owned Element
  ) -> (inserted: Bool, memberAfterInsert: Element) {
    let (bucket, found) = asNative.find(element)
    if found {
      return (false, asNative.uncheckedElement(at: bucket))
    }
    let isUnique = self.isUniquelyReferenced()
    asNative.insertNew(element, at: bucket, isUnique: isUnique)
    return (true, element)
  }

  internal mutating func remove(at index: Index) -> Element {
    let isUnique = isUniquelyReferenced()
    let bucket = asNative.validatedBucket(for: index)
    return asNative.uncheckedRemove(at: bucket, isUnique: isUnique)
  }

  internal mutating func remove(_ member: Element) -> Element? {
    let (bucket, found) = asNative.find(member)
    guard found else { return nil }
    let isUnique = isUniquelyReferenced()
    return asNative.uncheckedRemove(at: bucket, isUnique: isUnique)
  }

  internal mutating func removeAll(keepingCapacity keepCapacity: Bool) {
    if !keepCapacity {
      self = .init(native: _NativeSet<Element>())
      return
    }
    guard count > 0 else { return }

    let isUnique = isUniquelyReferenced()
    asNative.removeAll(isUnique: isUnique)
  }
}

extension Set._Variant {
  /// Returns an iterator over the elements.
  ///
  /// - Complexity: O(1).
  internal __consuming func makeIterator() -> Set<Element>.Iterator {
    return Set.Iterator(_native: asNative.makeIterator())
  }
}

