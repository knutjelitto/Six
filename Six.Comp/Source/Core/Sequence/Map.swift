//===--- Map.swift - Lazily map over a Sequence ---------------*- swift -*-===//
//
// This source file is part of the Swift.org open source project
//
// Copyright (c) 2014 - 2017 Apple Inc. and the Swift project authors
// Licensed under Apache License v2.0 with Runtime Library Exception
//
// See https://swift.org/LICENSE.txt for license information
// See https://swift.org/CONTRIBUTORS.txt for the list of Swift project authors
//
//===----------------------------------------------------------------------===//

/// A `Sequence` whose elements consist of those in a `Base`
/// `Sequence` passed through a transform function returning `Element`.
/// These elements are computed lazily, each time they're read, by
/// calling the transform function on a base element.
public struct LazyMapSequence<Base: Sequence, Element>
{
    public typealias Elements = LazyMapSequence

    internal var _base: Base
    internal let _transform: (Base.Element) -> Element

    /// Creates an instance with elements `transform(x)` for each element
    /// `x` of base.
    internal init(_base: Base, transform: (Base.Element) -> Element)
    {
        self._base = _base
        self._transform = transform
    }
}

extension LazyMapSequence
{
    public struct Iterator
    {
        internal var _base: Base.Iterator
        internal let _transform: (Base.Element) -> Element

        public var base: Base.Iterator { return _base }

        internal init(_base: Base.Iterator, _transform: (Base.Element) -> Element)
        {
            self._base = _base
            self._transform = _transform
        }
    }
}

extension LazyMapSequence.Iterator: IteratorProtocol, Sequence
{
      /// Advances to the next element and returns it, or `nil` if no next element
      /// exists.
      ///
      /// Once `nil` has been returned, all subsequent calls return `nil`.
      ///
      /// - Precondition: `next()` has not been applied to a copy of `self`
      ///   since the copy was made.
      public mutating func next() -> Element?
      {
            return _base.next().map(_transform)
      }
}

extension LazyMapSequence: LazySequenceProtocol
{
    /// Returns an iterator over the elements of this sequence.
    ///
    /// - Complexity: O(1).
    public __consuming func makeIterator() -> Iterator
    {
        return Iterator(_base: _base.makeIterator(), _transform: _transform)
    }

    /// A value less than or equal to the number of elements in the sequence,
    /// calculated nondestructively.
    ///
    /// The default implementation returns 0. If you provide your own
    /// implementation, make sure to compute the value nondestructively.
    ///
    /// - Complexity: O(1), except if the sequence also conforms to `Collection`.
    ///   In this case, see the documentation of `Collection.underestimatedCount`.
    public var underestimatedCount: Int
    {
        return _base.underestimatedCount
    }
}

/// A `Collection` whose elements consist of those in a `Base`
/// `Collection` passed through a transform function returning `Element`.
/// These elements are computed lazily, each time they're read, by
/// calling the transform function on a base element.
public typealias LazyMapCollection<T: Collection, U> = LazyMapSequence<T,U>

extension LazyMapCollection: Collection
{
    public typealias Index = Base.Index
    public typealias Indices = Base.Indices
    public typealias SubSequence = LazyMapCollection<Base.SubSequence, Element>

    public var startIndex: Base.Index { return _base.startIndex }
    public var endIndex: Base.Index { return _base.endIndex }

    public func index(after i: Index) -> Index { return _base.index(after: i) }
    public func formIndex(after i: inout Index) { _base.formIndex(after: &i) }

    /// Accesses the element at `position`.
    ///
    /// - Precondition: `position` is a valid position in `self` and
    ///   `position != endIndex`.
    public subscript(position: Base.Index) -> Element
    {
        return _transform(_base[position])
    }

    public subscript(bounds: Range<Base.Index>) -> SubSequence
    {
        return SubSequence(_base: _base[bounds], transform: _transform)
    }

    public var indices: Indices
    {
        return _base.indices
    }

    /// A Boolean value indicating whether the collection is empty.
    public var isEmpty: Bool { return _base.isEmpty }

    /// The number of elements in the collection.
    ///
    /// To check whether the collection is empty, use its `isEmpty` property
    /// instead of comparing `count` to zero. Unless the collection guarantees
    /// random-access performance, calculating `count` can be an O(*n*)
    /// operation.
    ///
    /// - Complexity: O(1) if `Index` conforms to `RandomAccessIndex`; O(*n*)
    ///   otherwise.
    public var count: Int
    {
        return _base.count
    }

    public func index(_ i: Index, offsetBy n: Int) -> Index
    {
        return _base.index(i, offsetBy: n)
    }

    public func index(_ i: Index, offsetBy n: Int, limitedBy limit: Index) -> Index?
    {
        return _base.index(i, offsetBy: n, limitedBy: limit)
    }

    public func distance(from start: Index, to end: Index) -> Int
    {
        return _base.distance(from: start, to: end)
    }
}

extension LazyMapCollection: BidirectionalCollection
    where Base: BidirectionalCollection
{

    /// A value less than or equal to the number of elements in the collection.
    ///
    /// - Complexity: O(1) if the collection conforms to
    ///   `RandomAccessCollection`; otherwise, O(*n*), where *n* is the length
    ///   of the collection.
    public func index(before i: Index) -> Index
    { 
    return _base.index(before: i)
    }

    public func formIndex(before i: inout Index)
    {
        _base.formIndex(before: &i)
    }
}

extension LazyMapCollection: LazyCollectionProtocol { }

extension LazyMapCollection: RandomAccessCollection
    where Base: RandomAccessCollection { }

//===--- Support for s.lazy -----------------------------------------------===//

extension LazySequenceProtocol
{
    /// Returns a `LazyMapSequence` over this `Sequence`.  The elements of
    /// the result are computed lazily, each time they are read, by
    /// calling `transform` function on a base element.
    public func map<U>(_ transform: (Element) -> U) -> LazyMapSequence<Elements, U>
    {
        return LazyMapSequence(_base: elements, transform: transform)
    }
}

extension LazyMapSequence
{
    public func map<ElementOfResult>(_ transform: (Element) -> ElementOfResult) -> LazyMapSequence<Base, ElementOfResult>
    {
        return LazyMapSequence<Base, ElementOfResult>(
            _base: _base,
            transform: { transform(self._transform($0)) })
    }
}

extension LazyMapCollection
{
    public func map<ElementOfResult>(_ transform: (Element) -> ElementOfResult) -> LazyMapCollection<Base, ElementOfResult>
    {
        return LazyMapCollection<Base, ElementOfResult>(
            _base: _base,
            transform: {transform(self._transform($0))})
    }
}
