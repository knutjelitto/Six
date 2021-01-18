//===----------------------------------------------------------------------===//
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

/// A collection of indices for an arbitrary collection
public struct DefaultIndices<Elements: Collection>
{
    internal var _elements: Elements
    internal var _startIndex: Elements.Index
    internal var _endIndex: Elements.Index

    internal init(_elements: Elements,
                  startIndex: Elements.Index,
                  endIndex: Elements.Index)
    {
        self._elements = _elements
        self._startIndex = startIndex
        self._endIndex = endIndex
    }
}

extension DefaultIndices: Collection
{
    public typealias Index = Elements.Index
    public typealias Element = Elements.Index
    public typealias Indices = DefaultIndices<Elements>
    public typealias SubSequence = DefaultIndices<Elements>
    public typealias Iterator = IndexingIterator<DefaultIndices<Elements>>

    public var startIndex: Index
    {
        return _startIndex
    }

    public var endIndex: Index
    {
        return _endIndex
    }

    public subscript(i: Index) -> Elements.Index
    {
        // FIXME: swift-3-indexing-model: range check.
        return i
    }

    public subscript(bounds: Range<Index>) -> DefaultIndices<Elements>
    {
        // FIXME: swift-3-indexing-model: range check.
        return DefaultIndices(
            _elements: _elements,
            startIndex: bounds.lowerBound,
            endIndex: bounds.upperBound)
    }

    public func index(after i: Index) -> Index
    {
        // FIXME: swift-3-indexing-model: range check.
        return _elements.index(after: i)
    }

    public func formIndex(after i: inout Index)
    {
        // FIXME: swift-3-indexing-model: range check.
        _elements.formIndex(after: &i)
    }

    public var indices: Indices
    {
        return self
    }
  
    public func index(_ i: Index, offsetBy distance: Int) -> Index
    {
        return _elements.index(i, offsetBy: distance)
    }

    public func index(_ i: Index, offsetBy distance: Int, limitedBy limit: Index) -> Index?
    {
        return _elements.index(i, offsetBy: distance, limitedBy: limit)
    }

    public func distance(from start: Index, to end: Index) -> Int
    {
        return _elements.distance(from: start, to: end)
    }
}

extension DefaultIndices: BidirectionalCollection
    where Elements: BidirectionalCollection
{
    public func index(before i: Index) -> Index
    {
        // FIXME: swift-3-indexing-model: range check.
        return _elements.index(before: i)
    }

    public func formIndex(before i: inout Index)
    {
        // FIXME: swift-3-indexing-model: range check.
        _elements.formIndex(before: &i)
    }
}

extension DefaultIndices: RandomAccessCollection
    where Elements: RandomAccessCollection
{
}

extension Collection
    where Indices == DefaultIndices<Self>
{
    /// The indices that are valid for subscripting the collection, in ascending
    /// order.
    ///
    /// A collection's `indices` property can hold a strong reference to the
    /// collection itself, causing the collection to be non-uniquely referenced.
    /// If you mutate the collection while iterating over its indices, a strong
    /// reference can cause an unexpected copy of the collection. To avoid the
    /// unexpected copy, use the `index(after:)` method starting with
    /// `startIndex` to produce indices instead.
    ///
    ///     var c = MyFancyCollection([10, 20, 30, 40, 50])
    ///     var i = c.startIndex
    ///     while i != c.endIndex {
    ///         c[i] /= 5
    ///         i = c.index(after: i)
    ///     }
    ///     // c == MyFancyCollection([2, 4, 6, 8, 10])
    public var indices: DefaultIndices<Self>
    {
        return DefaultIndices(
            _elements: self,
            startIndex: self.startIndex,
            endIndex: self.endIndex)
    }
}
