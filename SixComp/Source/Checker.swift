  public func index(after i: Index) -> Index {
    _precondition(i != endIndex, "Can't advance past endIndex")
    guard case .index(let i) = i._value else {
      _preconditionFailure("Invalid index passed to index(after:)")
    }
    let nextIndex = _base.index(after: i)
    guard nextIndex != _base.endIndex && _predicate(_base[nextIndex]) else {
      return Index(endOf: _base)
    }
    return Index(nextIndex)
  }
