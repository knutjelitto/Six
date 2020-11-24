  public func distance(from start: Index, to end: Index) -> Int
  {
    switch (start, end)
    {
    case let (.inRange(left), .inRange(right)):
      // in range <--> in range
      return numericCast(left.distance(to: right))
    case let (.inRange(left), .pastEnd):
      // in range --> end
      return numericCast(1 + left.distance(to: upperBound))
    case let (.pastEnd, .inRange(right)):
      // in range <-- end
      return numericCast(upperBound.distance(to: right) - 1)
    case (.pastEnd, .pastEnd):
      // end <--> end
      return 0
    }
  }
