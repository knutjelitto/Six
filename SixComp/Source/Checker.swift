  public init<Source: BinaryFloatingPoint>(_ value: Source) {
    // If two IEEE 754 binary interchange formats share the same exponent bit
    // count and significand bit count, then they must share the same encoding
    // for finite and infinite values.
    switch (Source.exponentBitCount, Source.significandBitCount) {
#if !os(macOS) && !(os(iOS) && targetEnvironment(macCatalyst))
#endif
    case (8, 23):
      let value_ = value as? Float ?? Float(
        sign: value.sign,
        exponentBitPattern:
          UInt(truncatingIfNeeded: value.exponentBitPattern),
        significandBitPattern:
          UInt32(truncatingIfNeeded: value.significandBitPattern))
      self = Self(value_)
    case (11, 52):
      let value_ = value as? Double ?? Double(
        sign: value.sign,
        exponentBitPattern:
          UInt(truncatingIfNeeded: value.exponentBitPattern),
        significandBitPattern:
          UInt64(truncatingIfNeeded: value.significandBitPattern))
      self = Self(value_)
#if !(os(Windows) || os(Android)) && (arch(i386) || arch(x86_64))
    case (15, 63):
      let value_ = value as? Float80 ?? Float80(
        sign: value.sign,
        exponentBitPattern:
          UInt(truncatingIfNeeded: value.exponentBitPattern),
        significandBitPattern:
          UInt64(truncatingIfNeeded: value.significandBitPattern))
      self = Self(value_)
#endif
    default:
      // Convert signaling NaN to quiet NaN by multiplying by 1.
      self = Self._convert(from: value).value * 1
    }
  }
