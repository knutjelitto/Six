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
internal protocol _DictionaryBuffer {
  associatedtype Key
  associatedtype Value
  associatedtype Index

  var startIndex: Index { get }
  var endIndex: Index { get }
  func index(after i: Index) -> Index
  func index(forKey key: Key) -> Index?
  var count: Int { get }

  func contains(_ key: Key) -> Bool
  func lookup(_ key: Key) -> Value?
  func lookup(_ index: Index) -> (key: Key, value: Value)
  func key(at index: Index) -> Key
  func value(at index: Index) -> Value
}

extension Dictionary {
  internal struct _Variant
  {
    internal var object: _BridgeStorage<__RawDictionaryStorage>

    init(native: __owned _NativeDictionary<Key, Value>) {
      self.object = _BridgeStorage(native: native._storage)
    }

    init(dummy: Void) {
      self.object = _BridgeStorage(taggedPayload: 0)
    }

  }
}

extension Dictionary._Variant {

  internal mutating func isUniquelyReferenced() -> Bool {
    return object.isUniquelyReferencedUnflaggedNative()
  }

  internal var asNative: _NativeDictionary<Key, Value> {
    get {
      return _NativeDictionary<Key, Value>(object.unflaggedNativeInstance)
    }
    set {
      self = .init(native: newValue)
    }
    _modify {
      var native = _NativeDictionary<Key, Value>(object.unflaggedNativeInstance)
      self = .init(dummy: ())
      defer { object = .init(native: native._storage) }
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

extension Dictionary._Variant: _DictionaryBuffer {
  internal typealias Element = (key: Key, value: Value)
  internal typealias Index = Dictionary<Key, Value>.Index

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

  internal func index(forKey key: Key) -> Index? {
    return asNative.index(forKey: key)
  }

  internal var count: Int {
    get {
      return asNative.count
    }
  }

  func contains(_ key: Key) -> Bool {
    return asNative.contains(key)
  }

  func lookup(_ key: Key) -> Value? {
    return asNative.lookup(key)
  }

  func lookup(_ index: Index) -> (key: Key, value: Value) {
    return asNative.lookup(index)
  }

  func key(at index: Index) -> Key {
    return asNative.key(at: index)
  }

  func value(at index: Index) -> Value {
    return asNative.value(at: index)
  }
}

extension Dictionary._Variant {
  internal subscript(key: Key) -> Value? {
    get {
      return lookup(key)
    }
    _modify {
      let isUnique = isUniquelyReferenced()
      yield &asNative[key, isUnique: isUnique]
    }
  }
}

extension Dictionary._Variant {
  /// Same as find(_:), except assume a corresponding key/value pair will be
  /// inserted if it doesn't already exist, and mutated if it does exist. When
  /// this function returns, the storage is guaranteed to be native, uniquely
  /// held, and with enough capacity for a single insertion (if the key isn't
  /// already in the dictionary.)
  internal mutating func mutatingFind(
    _ key: Key
  ) -> (bucket: _NativeDictionary<Key, Value>.Bucket, found: Bool) {
    let isUnique = isUniquelyReferenced()
    return asNative.mutatingFind(key, isUnique: isUnique)
  }

  internal mutating func ensureUniqueNative() -> _NativeDictionary<Key, Value> {
    let isUnique = isUniquelyReferenced()
    if !isUnique {
      asNative.copy()
    }
    return asNative
  }

  internal mutating func updateValue(
    _ value: __owned Value,
    forKey key: Key
  ) -> Value? {
    let isUnique = self.isUniquelyReferenced()
    return asNative.updateValue(value, forKey: key, isUnique: isUnique)
  }

  internal mutating func setValue(_ value: __owned Value, forKey key: Key) {
    let isUnique = self.isUniquelyReferenced()
    asNative.setValue(value, forKey: key, isUnique: isUnique)
  }

  internal mutating func remove(at index: Index) -> Element {
    // FIXME(performance): fuse data migration and element deletion into one
    // operation.
    let native = ensureUniqueNative()
    let bucket = native.validatedBucket(for: index)
    return asNative.uncheckedRemove(at: bucket, isUnique: true)
  }

  internal mutating func removeValue(forKey key: Key) -> Value? {
    let (bucket, found) = asNative.find(key)
    guard found else { return nil }
    let isUnique = isUniquelyReferenced()
    return asNative.uncheckedRemove(at: bucket, isUnique: isUnique).value
  }

  internal mutating func removeAll(keepingCapacity keepCapacity: Bool) {
    if !keepCapacity {
      self = .init(native: _NativeDictionary())
      return
    }
    guard count > 0 else { return }

    let isUnique = isUniquelyReferenced()
    asNative.removeAll(isUnique: isUnique)
  }
}

extension Dictionary._Variant {
  /// Returns an iterator over the `(Key, Value)` pairs.
  ///
  /// - Complexity: O(1).
  __consuming internal func makeIterator() -> Dictionary<Key, Value>.Iterator {
    return Dictionary.Iterator(_native: asNative.makeIterator())
  }
}

extension Dictionary._Variant {
  internal func mapValues<T>(
    _ transform: (Value) throws -> T
  ) rethrows -> _NativeDictionary<Key, T> {
    return try asNative.mapValues(transform)
  }

  internal mutating func merge<S: Sequence>(
    _ keysAndValues: __owned S,
    uniquingKeysWith combine: (Value, Value) throws -> Value
  ) rethrows where S.Element == (Key, Value) {
    let isUnique = isUniquelyReferenced()
    try asNative.merge(
      keysAndValues,
      isUnique: isUnique,
      uniquingKeysWith: combine)
  }
}

