class _MachineWords
  var bool1: Bool = true
  var bool2: Bool = false
  var i8: I8 = 0x3
  var i16: I16 = 0x7BCD
  var i32: I32 = 0x12345678
  var i64: I64 = 0x7EDCBA9876543210
  var i128: I128 = 0x7EDCBA9876543210123456789ABCDEFE
  var ilong: ILong = ILong(1) << (ILong(0).bitwidth() - 1)
  var isize: ISize = ISize(1) << (ISize(0).bitwidth() - 1)
  var f64: F64 = 9.82643431e19
  var f32: F32 = 1.2345e-13

  fun eq(that: _MachineWords box): Bool =>
    (bool1 == that.bool1)
      and (bool2 == that.bool2)
      and (i8 == that.i8)
      and (i16 == that.i16)
      and (i32 == that.i32)
      and (i64 == that.i64)
      and (i128 == that.i128)
      and (ilong == that.ilong)
      and (isize == that.isize)
      and (f32 == that.f32)
      and (f64 == that.f64)
