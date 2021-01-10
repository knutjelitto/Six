switch UInt8(number.objCType[0])
{
    case UInt8(ascii: "d"):
        return .double(number.doubleValue)
    case UInt8(ascii: "f"):
        return .float(number.floatValue)
    case UInt8(ascii: "Q"):
        return .uInt(number.unsignedLongLongValue)
    default:
        return .int(number.longLongValue)
}
