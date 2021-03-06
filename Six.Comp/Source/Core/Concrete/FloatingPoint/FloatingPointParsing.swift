// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 1)
//===----------------------------------------------------------------------===//
//
// This source file is part of the Swift.org open source project
//
// Copyright (c) 2014 - 2019 Apple Inc. and the Swift project authors
// Licensed under Apache License v2.0 with Runtime Library Exception
//
// See https://swift.org/LICENSE.txt for license information
// See https://swift.org/CONTRIBUTORS.txt for the list of Swift project authors
//
//===----------------------------------------------------------------------===//

import SwiftShims

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 32)

/// Returns `true` iff isspace(u) would return nonzero when the current
/// locale is the C locale.
internal func _isspace_clocale(_ u: UTF16.CodeUnit) -> Bool {
  return "\t\n\u{b}\u{c}\r ".utf16.contains(u)
}

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 42)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 46)

//===--- Parsing ----------------------------------------------------------===//
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 49)
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 53)
extension Float16: LosslessStringConvertible {
  /// Creates a new instance from the given string.
  ///
  /// The string passed as `text` can represent a real number in decimal or
  /// hexadecimal format or special floating-point values for infinity and NaN
  /// ("not a number").
  ///
  /// The given string may begin with a plus or minus sign character (`+` or
  /// `-`). The allowed formats for each of these representations is then as
  /// follows:
  ///
  /// - A *decimal value* contains the significand, a sequence of decimal
  ///   digits that may include a decimal point.
  ///
  ///       let c = Float16("-1.0")
  ///       // c == -1.0
  ///
  ///       let d = Float16("28.375")
  ///       // d == 28.375
  ///
  ///   A decimal value may also include an exponent following the significand,
  ///   indicating the power of 10 by which the significand should be
  ///   multiplied. If included, the exponent is separated by a single
  ///   character, `e` or `E`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let e = Float16("2837.5e-2")
  ///       // e == 28.375
  ///
  /// - A *hexadecimal value* contains the significand, either `0X` or `0x`,
  ///   followed by a sequence of hexadecimal digits. The significand may
  ///   include a decimal point.
  ///
  ///       let f = Float16("0x1c.6")
  ///       // f == 28.375
  ///
  ///   A hexadecimal value may also include an exponent following the
  ///   significand, indicating the power of 2 by which the significand should
  ///   be multiplied. If included, the exponent is separated by a single
  ///   character, `p` or `P`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let g = Float16("0x1.c6p4")
  ///       // g == 28.375
  ///
  /// - A value of *infinity* contains one of the strings `"inf"` or
  ///   `"infinity"`, case insensitive.
  ///
  ///       let i = Float16("inf")
  ///       // i == Float16.infinity
  ///
  ///       let j = Float16("-Infinity")
  ///       // j == -Float16.infinity
  ///
  /// - A value of *NaN* contains the string `"nan"`, case insensitive.
  ///
  ///       let n = Float16("-nan")
  ///       // n?.isNaN == true
  ///       // n?.sign == .minus
  ///
  ///   A NaN value may also include a payload in parentheses following the
  ///   `"nan"` keyword. The payload consists of a sequence of decimal digits,
  ///   or the characters `0X` or `0x` followed by a sequence of hexadecimal
  ///   digits. If the payload contains any other characters, it is ignored.
  ///   If the value of the payload is larger than can be stored as the
  ///   payload of a `Float16.nan`, the least significant bits are used.
  ///
  ///       let p = Float16("nan(0x10)")
  ///       // p?.isNaN == true
  ///       // String(p!) == "nan(0x10)"
  ///
  /// Passing any other format or any additional characters as `text` results
  /// in `nil`. For example, the following conversions result in `nil`:
  ///
  ///     Float16(" 5.0")      // Includes whitespace
  ///     Float16("±2.0")      // Invalid character
  ///     Float16("0x1.25e4")  // Incorrect exponent format
  ///
  /// - Parameter text: The input string to convert to a `Float16` instance. If
  ///   `text` has invalid characters or is in an invalid format, the result
  ///   is `nil`.
  public init?<S: StringProtocol>(_ text: S) {
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 137)
    self.init(Substring(text))
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 163)
  }

  // Caveat:  This implementation used to be inlineable.
  // In particular, we still have to export
  // _swift_stdlib_strtoXYZ_clocale()
  // as ABI to support old compiled code that still requires it.
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 170)
  @available(iOS 14.0, watchOS 7.0, tvOS 14.0, *)
  @available(macOS, unavailable)
  @available(macCatalyst, unavailable)
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 176)
  public init?(_ text: Substring) {
    self = 0.0
    let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
      text.withCString { chars -> Bool in
        switch chars[0] {
        case 9, 10, 11, 12, 13, 32:
          return false // Reject leading whitespace
        case 0:
          return false // Reject empty string
        default:
          break
        }
        let endPtr = _swift_stdlib_strtof16_clocale(chars, p)
        // Succeed only if endPtr points to end of C string
        return endPtr != nil && endPtr![0] == 0
      }
    }
    if !success {
      return nil
    }
  }
}

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 202)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 42)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 46)

//===--- Parsing ----------------------------------------------------------===//
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 53)
extension Float: LosslessStringConvertible {
  /// Creates a new instance from the given string.
  ///
  /// The string passed as `text` can represent a real number in decimal or
  /// hexadecimal format or special floating-point values for infinity and NaN
  /// ("not a number").
  ///
  /// The given string may begin with a plus or minus sign character (`+` or
  /// `-`). The allowed formats for each of these representations is then as
  /// follows:
  ///
  /// - A *decimal value* contains the significand, a sequence of decimal
  ///   digits that may include a decimal point.
  ///
  ///       let c = Float("-1.0")
  ///       // c == -1.0
  ///
  ///       let d = Float("28.375")
  ///       // d == 28.375
  ///
  ///   A decimal value may also include an exponent following the significand,
  ///   indicating the power of 10 by which the significand should be
  ///   multiplied. If included, the exponent is separated by a single
  ///   character, `e` or `E`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let e = Float("2837.5e-2")
  ///       // e == 28.375
  ///
  /// - A *hexadecimal value* contains the significand, either `0X` or `0x`,
  ///   followed by a sequence of hexadecimal digits. The significand may
  ///   include a decimal point.
  ///
  ///       let f = Float("0x1c.6")
  ///       // f == 28.375
  ///
  ///   A hexadecimal value may also include an exponent following the
  ///   significand, indicating the power of 2 by which the significand should
  ///   be multiplied. If included, the exponent is separated by a single
  ///   character, `p` or `P`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let g = Float("0x1.c6p4")
  ///       // g == 28.375
  ///
  /// - A value of *infinity* contains one of the strings `"inf"` or
  ///   `"infinity"`, case insensitive.
  ///
  ///       let i = Float("inf")
  ///       // i == Float.infinity
  ///
  ///       let j = Float("-Infinity")
  ///       // j == -Float.infinity
  ///
  /// - A value of *NaN* contains the string `"nan"`, case insensitive.
  ///
  ///       let n = Float("-nan")
  ///       // n?.isNaN == true
  ///       // n?.sign == .minus
  ///
  ///   A NaN value may also include a payload in parentheses following the
  ///   `"nan"` keyword. The payload consists of a sequence of decimal digits,
  ///   or the characters `0X` or `0x` followed by a sequence of hexadecimal
  ///   digits. If the payload contains any other characters, it is ignored.
  ///   If the value of the payload is larger than can be stored as the
  ///   payload of a `Float.nan`, the least significant bits are used.
  ///
  ///       let p = Float("nan(0x10)")
  ///       // p?.isNaN == true
  ///       // String(p!) == "nan(0x10)"
  ///
  /// Passing any other format or any additional characters as `text` results
  /// in `nil`. For example, the following conversions result in `nil`:
  ///
  ///     Float(" 5.0")      // Includes whitespace
  ///     Float("±2.0")      // Invalid character
  ///     Float("0x1.25e4")  // Incorrect exponent format
  ///
  /// - Parameter text: The input string to convert to a `Float` instance. If
  ///   `text` has invalid characters or is in an invalid format, the result
  ///   is `nil`.
  @inlinable
  public init?<S: StringProtocol>(_ text: S) {
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 139)
    if #available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *) {
      self.init(Substring(text))
    } else {
      self = 0.0
      let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
        text.withCString { chars -> Bool in
          switch chars[0] {
          case 9, 10, 11, 12, 13, 32:
            return false // Reject leading whitespace
          case 0:
            return false // Reject empty string
          default:
            break
          }
          let endPtr = _swift_stdlib_strtof_clocale(chars, p)
          // Succeed only if endPtr points to end of C string
          return endPtr != nil && endPtr![0] == 0
        }
      }
      if !success {
        return nil
      }
    }
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 163)
  }

  // Caveat:  This implementation used to be inlineable.
  // In particular, we still have to export
  // _swift_stdlib_strtoXYZ_clocale()
  // as ABI to support old compiled code that still requires it.
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 174)
  @available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *)
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 176)
  public init?(_ text: Substring) {
    self = 0.0
    let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
      text.withCString { chars -> Bool in
        switch chars[0] {
        case 9, 10, 11, 12, 13, 32:
          return false // Reject leading whitespace
        case 0:
          return false // Reject empty string
        default:
          break
        }
        let endPtr = _swift_stdlib_strtof_clocale(chars, p)
        // Succeed only if endPtr points to end of C string
        return endPtr != nil && endPtr![0] == 0
      }
    }
    if !success {
      return nil
    }
  }
}

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 202)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 42)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 46)

//===--- Parsing ----------------------------------------------------------===//
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 53)
extension Double: LosslessStringConvertible {
  /// Creates a new instance from the given string.
  ///
  /// The string passed as `text` can represent a real number in decimal or
  /// hexadecimal format or special floating-point values for infinity and NaN
  /// ("not a number").
  ///
  /// The given string may begin with a plus or minus sign character (`+` or
  /// `-`). The allowed formats for each of these representations is then as
  /// follows:
  ///
  /// - A *decimal value* contains the significand, a sequence of decimal
  ///   digits that may include a decimal point.
  ///
  ///       let c = Double("-1.0")
  ///       // c == -1.0
  ///
  ///       let d = Double("28.375")
  ///       // d == 28.375
  ///
  ///   A decimal value may also include an exponent following the significand,
  ///   indicating the power of 10 by which the significand should be
  ///   multiplied. If included, the exponent is separated by a single
  ///   character, `e` or `E`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let e = Double("2837.5e-2")
  ///       // e == 28.375
  ///
  /// - A *hexadecimal value* contains the significand, either `0X` or `0x`,
  ///   followed by a sequence of hexadecimal digits. The significand may
  ///   include a decimal point.
  ///
  ///       let f = Double("0x1c.6")
  ///       // f == 28.375
  ///
  ///   A hexadecimal value may also include an exponent following the
  ///   significand, indicating the power of 2 by which the significand should
  ///   be multiplied. If included, the exponent is separated by a single
  ///   character, `p` or `P`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let g = Double("0x1.c6p4")
  ///       // g == 28.375
  ///
  /// - A value of *infinity* contains one of the strings `"inf"` or
  ///   `"infinity"`, case insensitive.
  ///
  ///       let i = Double("inf")
  ///       // i == Double.infinity
  ///
  ///       let j = Double("-Infinity")
  ///       // j == -Double.infinity
  ///
  /// - A value of *NaN* contains the string `"nan"`, case insensitive.
  ///
  ///       let n = Double("-nan")
  ///       // n?.isNaN == true
  ///       // n?.sign == .minus
  ///
  ///   A NaN value may also include a payload in parentheses following the
  ///   `"nan"` keyword. The payload consists of a sequence of decimal digits,
  ///   or the characters `0X` or `0x` followed by a sequence of hexadecimal
  ///   digits. If the payload contains any other characters, it is ignored.
  ///   If the value of the payload is larger than can be stored as the
  ///   payload of a `Double.nan`, the least significant bits are used.
  ///
  ///       let p = Double("nan(0x10)")
  ///       // p?.isNaN == true
  ///       // String(p!) == "nan(0x10)"
  ///
  /// Passing any other format or any additional characters as `text` results
  /// in `nil`. For example, the following conversions result in `nil`:
  ///
  ///     Double(" 5.0")      // Includes whitespace
  ///     Double("±2.0")      // Invalid character
  ///     Double("0x1.25e4")  // Incorrect exponent format
  ///
  /// - Parameter text: The input string to convert to a `Double` instance. If
  ///   `text` has invalid characters or is in an invalid format, the result
  ///   is `nil`.
  @inlinable
  public init?<S: StringProtocol>(_ text: S) {
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 139)
    if #available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *) {
      self.init(Substring(text))
    } else {
      self = 0.0
      let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
        text.withCString { chars -> Bool in
          switch chars[0] {
          case 9, 10, 11, 12, 13, 32:
            return false // Reject leading whitespace
          case 0:
            return false // Reject empty string
          default:
            break
          }
          let endPtr = _swift_stdlib_strtod_clocale(chars, p)
          // Succeed only if endPtr points to end of C string
          return endPtr != nil && endPtr![0] == 0
        }
      }
      if !success {
        return nil
      }
    }
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 163)
  }

  // Caveat:  This implementation used to be inlineable.
  // In particular, we still have to export
  // _swift_stdlib_strtoXYZ_clocale()
  // as ABI to support old compiled code that still requires it.
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 174)
  @available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *)
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 176)
  public init?(_ text: Substring) {
    self = 0.0
    let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
      text.withCString { chars -> Bool in
        switch chars[0] {
        case 9, 10, 11, 12, 13, 32:
          return false // Reject leading whitespace
        case 0:
          return false // Reject empty string
        default:
          break
        }
        let endPtr = _swift_stdlib_strtod_clocale(chars, p)
        // Succeed only if endPtr points to end of C string
        return endPtr != nil && endPtr![0] == 0
      }
    }
    if !success {
      return nil
    }
  }
}

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 202)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 42)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 44)
#if !(os(Windows) || os(Android)) && (arch(i386) || arch(x86_64))
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 46)

//===--- Parsing ----------------------------------------------------------===//
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 53)
extension Float80: LosslessStringConvertible {
  /// Creates a new instance from the given string.
  ///
  /// The string passed as `text` can represent a real number in decimal or
  /// hexadecimal format or special floating-point values for infinity and NaN
  /// ("not a number").
  ///
  /// The given string may begin with a plus or minus sign character (`+` or
  /// `-`). The allowed formats for each of these representations is then as
  /// follows:
  ///
  /// - A *decimal value* contains the significand, a sequence of decimal
  ///   digits that may include a decimal point.
  ///
  ///       let c = Float80("-1.0")
  ///       // c == -1.0
  ///
  ///       let d = Float80("28.375")
  ///       // d == 28.375
  ///
  ///   A decimal value may also include an exponent following the significand,
  ///   indicating the power of 10 by which the significand should be
  ///   multiplied. If included, the exponent is separated by a single
  ///   character, `e` or `E`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let e = Float80("2837.5e-2")
  ///       // e == 28.375
  ///
  /// - A *hexadecimal value* contains the significand, either `0X` or `0x`,
  ///   followed by a sequence of hexadecimal digits. The significand may
  ///   include a decimal point.
  ///
  ///       let f = Float80("0x1c.6")
  ///       // f == 28.375
  ///
  ///   A hexadecimal value may also include an exponent following the
  ///   significand, indicating the power of 2 by which the significand should
  ///   be multiplied. If included, the exponent is separated by a single
  ///   character, `p` or `P`, and consists of an optional plus or minus sign
  ///   character and a sequence of decimal digits.
  ///
  ///       let g = Float80("0x1.c6p4")
  ///       // g == 28.375
  ///
  /// - A value of *infinity* contains one of the strings `"inf"` or
  ///   `"infinity"`, case insensitive.
  ///
  ///       let i = Float80("inf")
  ///       // i == Float80.infinity
  ///
  ///       let j = Float80("-Infinity")
  ///       // j == -Float80.infinity
  ///
  /// - A value of *NaN* contains the string `"nan"`, case insensitive.
  ///
  ///       let n = Float80("-nan")
  ///       // n?.isNaN == true
  ///       // n?.sign == .minus
  ///
  ///   A NaN value may also include a payload in parentheses following the
  ///   `"nan"` keyword. The payload consists of a sequence of decimal digits,
  ///   or the characters `0X` or `0x` followed by a sequence of hexadecimal
  ///   digits. If the payload contains any other characters, it is ignored.
  ///   If the value of the payload is larger than can be stored as the
  ///   payload of a `Float80.nan`, the least significant bits are used.
  ///
  ///       let p = Float80("nan(0x10)")
  ///       // p?.isNaN == true
  ///       // String(p!) == "nan(0x10)"
  ///
  /// Passing any other format or any additional characters as `text` results
  /// in `nil`. For example, the following conversions result in `nil`:
  ///
  ///     Float80(" 5.0")      // Includes whitespace
  ///     Float80("±2.0")      // Invalid character
  ///     Float80("0x1.25e4")  // Incorrect exponent format
  ///
  /// - Parameter text: The input string to convert to a `Float80` instance. If
  ///   `text` has invalid characters or is in an invalid format, the result
  ///   is `nil`.
  @inlinable
  public init?<S: StringProtocol>(_ text: S) {
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 139)
    if #available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *) {
      self.init(Substring(text))
    } else {
      self = 0.0
      let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
        text.withCString { chars -> Bool in
          switch chars[0] {
          case 9, 10, 11, 12, 13, 32:
            return false // Reject leading whitespace
          case 0:
            return false // Reject empty string
          default:
            break
          }
          let endPtr = _swift_stdlib_strtold_clocale(chars, p)
          // Succeed only if endPtr points to end of C string
          return endPtr != nil && endPtr![0] == 0
        }
      }
      if !success {
        return nil
      }
    }
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 163)
  }

  // Caveat:  This implementation used to be inlineable.
  // In particular, we still have to export
  // _swift_stdlib_strtoXYZ_clocale()
  // as ABI to support old compiled code that still requires it.
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 174)
  @available(macOS 10.16, iOS 14.0, watchOS 7.0, tvOS 14.0, *)
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 176)
  public init?(_ text: Substring) {
    self = 0.0
    let success = withUnsafeMutablePointer(to: &self) { p -> Bool in
      text.withCString { chars -> Bool in
        switch chars[0] {
        case 9, 10, 11, 12, 13, 32:
          return false // Reject leading whitespace
        case 0:
          return false // Reject empty string
        default:
          break
        }
        let endPtr = _swift_stdlib_strtold_clocale(chars, p)
        // Succeed only if endPtr points to end of C string
        return endPtr != nil && endPtr![0] == 0
      }
    }
    if !success {
      return nil
    }
  }
}

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 200)
#endif
// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 202)

// ###sourceLocation(file: "D:/Projects/Six/Six.Comp/Source/Core/Concrete/FloatingPoint/FloatingPointParsing.swift.gyb", line: 204)

// Local Variables:
// eval: (read-only-mode 1)
// End:
