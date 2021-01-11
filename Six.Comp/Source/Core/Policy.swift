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
// Swift Standard Prolog Library.
//===----------------------------------------------------------------------===//

//===----------------------------------------------------------------------===//
// Standardized uninhabited type
//===----------------------------------------------------------------------===//
/// The return type of functions that do not return normally, that is, a type
/// with no values.
///
/// Use `Never` as the return type when declaring a closure, function, or
/// method that unconditionally throws an error, traps, or otherwise does
/// not terminate.
///
///     func crashAndBurn() -> Never {
///         fatalError("Something very, very bad happened")
///     }
public enum Never {}

extension Never: Error {}

extension Never: Equatable, Comparable, Hashable {}

//===----------------------------------------------------------------------===//
// Standardized aliases
//===----------------------------------------------------------------------===//
/// The return type of functions that don't explicitly specify a return type,
/// that is, an empty tuple `()`.
///
/// When declaring a function or method, you don't need to specify a return
/// type if no value will be returned. However, the type of a function,
/// method, or closure always includes a return type, which is `Void` if
/// otherwise unspecified.
///
/// Use `Void` or an empty tuple as the return type when declaring a closure,
/// function, or method that doesn't return a value.
///
///     // No return type declared:
///     func logMessage(_ s: String) {
///         print("Message: \(s)")
///     }
///
///     let logger: (String) -> Void = logMessage
///     logger("This is a void function")
///     // Prints "Message: This is a void function"
public typealias Void = ()

//===----------------------------------------------------------------------===//
// Aliases for floating point types
//===----------------------------------------------------------------------===//
// FIXME: it should be the other way round, Float = Float32, Double = Float64,
// but the type checker loses sugar currently, and ends up displaying 'FloatXX'
// in diagnostics.
/// A 32-bit floating point type.
public typealias Float32 = Float
/// A 64-bit floating point type.
public typealias Float64 = Double

//===----------------------------------------------------------------------===//
// Default types for unconstrained literals
//===----------------------------------------------------------------------===//
/// The default type for an otherwise-unconstrained integer literal.
public typealias IntegerLiteralType = Int
/// The default type for an otherwise-unconstrained floating point literal.
public typealias FloatLiteralType = Double

/// The default type for an otherwise-unconstrained Boolean literal.
///
/// When you create a constant or variable using one of the Boolean literals
/// `true` or `false`, the resulting type is determined by the
/// `BooleanLiteralType` alias. For example:
///
///     let isBool = true
///     print("isBool is a '\(type(of: isBool))'")
///     // Prints "isBool is a 'Bool'"
///
/// The type aliased by `BooleanLiteralType` must conform to the
/// `ExpressibleByBooleanLiteral` protocol.
public typealias BooleanLiteralType = Bool

/// The default type for an otherwise-unconstrained unicode scalar literal.
public typealias UnicodeScalarType = String
/// The default type for an otherwise-unconstrained Unicode extended
/// grapheme cluster literal.
public typealias ExtendedGraphemeClusterType = String
/// The default type for an otherwise-unconstrained string literal.
public typealias StringLiteralType = String

//===----------------------------------------------------------------------===//
// Default types for unconstrained number literals
//===----------------------------------------------------------------------===//
public typealias _MaxBuiltinFloatType = Builtin.FPIEEE64

//===----------------------------------------------------------------------===//
// Standard protocols
//===----------------------------------------------------------------------===//

/// The protocol to which all classes implicitly conform.
public typealias AnyObject = Builtin.AnyObject

/// The protocol to which all class types implicitly conform.
///
/// You can use the `AnyClass` protocol as the concrete type for an instance of
/// any class. When you do, all known `@objc` class methods and properties are
/// available as implicitly unwrapped optional methods and properties,
/// respectively. For example:
///
///     class IntegerRef {
///         @objc class func getDefaultValue() -> Int {
///             return 42
///         }
///     }
///
///     func getDefaultValue(_ c: AnyClass) -> Int? {
///         return c.getDefaultValue?()
///     }
///
/// The `getDefaultValue(_:)` function uses optional chaining to safely call
/// the implicitly unwrapped class method on `c`. Calling the function with
/// different class types shows how the `getDefaultValue()` class method is
/// only conditionally available.
///
///     print(getDefaultValue(IntegerRef.self))
///     // Prints "Optional(42)"
///
///     print(getDefaultValue(NSString.self))
///     // Prints "nil"
public typealias AnyClass = AnyObject.Type

//===----------------------------------------------------------------------===//
// Standard pattern matching forms
//===----------------------------------------------------------------------===//

/// Returns a Boolean value indicating whether two arguments match by value
/// equality.
///
/// The pattern-matching operator (`~=`) is used internally in `case`
/// statements for pattern matching. When you match against an `Equatable`
/// value in a `case` statement, this operator is called behind the scenes.
///
///     let weekday = 3
///     let lunch: String
///     switch weekday {
///     case 3:
///         lunch = "Taco Tuesday!"
///     default:
///         lunch = "Pizza again."
///     }
///     // lunch == "Taco Tuesday!"
///
/// In this example, the `case 3` expression uses this pattern-matching
/// operator to test whether `weekday` is equal to the value `3`.
///
/// - Note: In most cases, you should use the equal-to operator (`==`) to test
///   whether two instances are equal. The pattern-matching operator is
///   primarily intended to enable `case` statement pattern matching.
///
/// - Parameters:
///   - lhs: A value to compare.
///   - rhs: Another value to compare.
public func ~= <T: Equatable>(a: T, b: T) -> Bool {
  return a == b
}

//===----------------------------------------------------------------------===//
// Standard precedence groups
//===----------------------------------------------------------------------===//

precedencegroup AssignmentPrecedence {
  assignment: true
  associativity: right
}
precedencegroup FunctionArrowPrecedence {
  associativity: right
  higherThan: AssignmentPrecedence
}
precedencegroup TernaryPrecedence {
  associativity: right
  higherThan: FunctionArrowPrecedence
}
precedencegroup DefaultPrecedence {
  higherThan: TernaryPrecedence
}
precedencegroup LogicalDisjunctionPrecedence {
  associativity: left
  higherThan: TernaryPrecedence
}
precedencegroup LogicalConjunctionPrecedence {
  associativity: left
  higherThan: LogicalDisjunctionPrecedence
}
precedencegroup ComparisonPrecedence {
  higherThan: LogicalConjunctionPrecedence
}
precedencegroup NilCoalescingPrecedence {
  associativity: right
  higherThan: ComparisonPrecedence
}
precedencegroup CastingPrecedence {
  higherThan: NilCoalescingPrecedence
}
precedencegroup RangeFormationPrecedence {
  higherThan: CastingPrecedence
}
precedencegroup AdditionPrecedence {
  associativity: left
  higherThan: RangeFormationPrecedence
}
precedencegroup MultiplicationPrecedence {
  associativity: left
  higherThan: AdditionPrecedence
}
precedencegroup BitwiseShiftPrecedence {
  higherThan: MultiplicationPrecedence
}


//===----------------------------------------------------------------------===//
// Standard operators
//===----------------------------------------------------------------------===//

// Standard postfix operators.
postfix operator ++
postfix operator --
postfix operator ...: Comparable

// Optional<T> unwrapping operator is built into the compiler as a part of
// postfix expression grammar.
//
// postfix operator !

// Standard prefix operators.
prefix operator ++
prefix operator --
prefix operator !: Bool
prefix operator ~: BinaryInteger
prefix operator +: AdditiveArithmetic
prefix operator -: SignedNumeric
prefix operator ...: Comparable
prefix operator ..<: Comparable

// Standard infix operators.

// "Exponentiative"

infix operator  <<: BitwiseShiftPrecedence, BinaryInteger
infix operator &<<: BitwiseShiftPrecedence, FixedWidthInteger
infix operator  >>: BitwiseShiftPrecedence, BinaryInteger
infix operator &>>: BitwiseShiftPrecedence, FixedWidthInteger

// "Multiplicative"

infix operator   *: MultiplicationPrecedence, Numeric
infix operator  &*: MultiplicationPrecedence, FixedWidthInteger
infix operator   /: MultiplicationPrecedence, BinaryInteger, FloatingPoint
infix operator   %: MultiplicationPrecedence, BinaryInteger
infix operator   &: MultiplicationPrecedence, BinaryInteger

// "Additive"

infix operator   +: AdditionPrecedence, AdditiveArithmetic, String, Array, Strideable
infix operator  &+: AdditionPrecedence, FixedWidthInteger
infix operator   -: AdditionPrecedence, AdditiveArithmetic, Strideable
infix operator  &-: AdditionPrecedence, FixedWidthInteger
infix operator   |: AdditionPrecedence, BinaryInteger
infix operator   ^: AdditionPrecedence, BinaryInteger

// FIXME: is this the right precedence level for "..." ?
infix operator  ...: RangeFormationPrecedence, Comparable
infix operator  ..<: RangeFormationPrecedence, Comparable

// The cast operators 'as' and 'is' are hardcoded as if they had the
// following attributes:
// infix operator as: CastingPrecedence

// "Coalescing"

infix operator ??: NilCoalescingPrecedence

// "Comparative"

infix operator  <: ComparisonPrecedence, Comparable
infix operator  <=: ComparisonPrecedence, Comparable
infix operator  >: ComparisonPrecedence, Comparable
infix operator  >=: ComparisonPrecedence, Comparable
infix operator  ==: ComparisonPrecedence, Equatable
infix operator  !=: ComparisonPrecedence, Equatable
infix operator ===: ComparisonPrecedence
infix operator !==: ComparisonPrecedence
// FIXME: ~= will be built into the compiler.
infix operator  ~=: ComparisonPrecedence

// "Conjunctive"

infix operator &&: LogicalConjunctionPrecedence, Bool

// "Disjunctive"

infix operator ||: LogicalDisjunctionPrecedence, Bool

// User-defined ternary operators are not supported. The ? : operator is
// hardcoded as if it had the following attributes:
// operator ternary ? : : TernaryPrecedence

// User-defined assignment operators are not supported. The = operator is
// hardcoded as if it had the following attributes:
// infix operator =: AssignmentPrecedence

// Compound

infix operator   *=: AssignmentPrecedence, Numeric
infix operator  &*=: AssignmentPrecedence, FixedWidthInteger
infix operator   /=: AssignmentPrecedence, BinaryInteger
infix operator   %=: AssignmentPrecedence, BinaryInteger
infix operator   +=: AssignmentPrecedence, AdditiveArithmetic, String, Array, Strideable
infix operator  &+=: AssignmentPrecedence, FixedWidthInteger
infix operator   -=: AssignmentPrecedence, AdditiveArithmetic, Strideable
infix operator  &-=: AssignmentPrecedence, FixedWidthInteger
infix operator  <<=: AssignmentPrecedence, BinaryInteger
infix operator &<<=: AssignmentPrecedence, FixedWidthInteger
infix operator  >>=: AssignmentPrecedence, BinaryInteger
infix operator &>>=: AssignmentPrecedence, FixedWidthInteger
infix operator   &=: AssignmentPrecedence, BinaryInteger
infix operator   ^=: AssignmentPrecedence, BinaryInteger
infix operator   |=: AssignmentPrecedence, BinaryInteger

// Workaround for <rdar://problem/14011860> SubTLF: Default
// implementations in protocols.  Library authors should ensure
// that this operator never needs to be seen by end-users.  See
// test/Prototypes/GenericDispatch.swift for a fully documented
// example of how this operator is used, and how its use can be hidden
// from users.
infix operator ~>
