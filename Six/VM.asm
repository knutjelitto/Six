;;
;; ABI - REGISTERS
;;
;;             [    WIN64    ]      ----VM----
;; RAX      -- var  -- Result       var
;; RBX      -- fix                  var
;; RCX      -- var  -- Param 1      var
;; RDX      -- var  -- Param 2      var
;; RSI      -- fix                  Instruction pointer
;; RDI      -- fix                  Stack pointer
;; RBP      -- fix
;; RSP      -- fix
;; R8       -- var  -- Param 3
;; R9       -- var  -- Param 4
;; R10      -- var
;; R11      -- var
;; R12      -- fix
;; R13      -- fix
;; R14      -- fix
;; R15      -- fix
;;
;; XMM0     -- var  -- Param 1
;; XMM1     -- var  -- Param 2
;; XMM2     -- var  -- Param 3
;; XMM3     -- var  -- Param 4
;; XMM4     -- var
;; XMM5     -- var
;; XMM6     -- fix
;; XMM7     -- fix
;; XMM8     -- fix
;; XMM9     -- fix
;; XMM10    -- fix
;; XMM11    -- fix
;; XMM12    -- fix
;; XMM13    -- fix
;; XMM14    -- fix
;; XMM15    -- fix
;;


format PE64 console DLL
entry DllEntryPoint

include 'win64ax.inc'

section '.text' code readable executable

proc DllEntryPoint hinstDLL,fdwReason,lpvReserved
    mov     rax,TRUE
    ret
endp

Operations:
OpNop = 0
    dq      ExecNop
OpI8  = 1
    dq      ExecI8
OpI16 = 2
    dq      ExecI16
OpI32 = 3
    dq      ExecI32
OpI64 = 4
    dq      ExecI64
OpAdd = 5
    dq      ExecAdd
OpRet = 6
    dq      ExecRet

; (addr) -> (stack-usage)
proc Execute uses rbx rsi rdi, chunk
    mov     [chunk], rcx
    mov     rsi, rcx
    mov     rdi, [StackTop]
    call    InnerExec
    mov     [StackTop], rdi
    mov     rax, StackEnd
    sub     rax, rdi
    ret
endp

; (index) -> (value)
proc GetStackAt index
    mov     rax, [StackTop]
    mov     rax, [rax + rcx*8]
    ret
endp

InnerExec:
    movzx   rax, byte [rsi]
    inc     rsi
    call    qword [Operations + rax*8]
    jmp     InnerExec
Return:
    ret

ExecNop:
    ret

ExecI8:
    movsx   rax, byte [rsi]
    add     rsi, 1
    sub     rdi, 8
    mov     [rdi], rax
    ret

ExecI16:
    movsx   rax, word [rsi]
    add     rsi, 2
    sub     rdi, 8
    mov     [rdi], rax
    ret

ExecI32:
    movsxd  rax, dword [rsi]
    add     rsi, 4
    sub     rdi, 8
    mov     [rdi], rax
    ret

ExecI64:
    mov     rax, qword [rsi]
    add     rsi, 8
    sub     rdi, 8
    mov     [rdi], rax
    ret

ExecAdd:
    mov     rax, [rdi]
    add     rax, [rdi + 8]
    add     rdi, 8
    mov     [rdi], rax
    ret;

ExecRet:
    pop     rax
    mov     rax, 42
    jmp     Return

proc EmitNop    ; (addr) -> (#Emitted)
    mov     [rcx], byte OpNop
    mov     rax, 1
    ret
endp

proc EmitI8  ; (addr, i8) -> (#Emitted)
    mov     [rcx], byte OpI8
    inc     rcx
    mov     [rcx], rdx
    mov     rax, 2
    ret
endp

proc EmitI16  ; (addr, i16) -> (#Emitted)
    mov     [rcx], byte OpI16
    inc     rcx
    mov     [rcx], rdx
    mov     rax, 3
    ret
endp

proc EmitI32  ; (addr, i32) -> (#Emitted)
    mov     [rcx], byte OpI32
    inc     rcx
    mov     [rcx], rdx
    mov     rax, 5
    ret
endp

proc EmitI64  ; (addr, i64) -> (#Emitted)
    mov     [rcx], byte OpI64
    inc     rcx
    mov     [rcx], rdx
    mov     rax, 9
    ret
endp

proc EmitAdd    ; (addr) -> (#Emitted)
    mov     [rcx], byte OpAdd
    mov     rax, 1
    ret
endp

proc EmitRet    ; (addr) -> (#Emitted)
    mov     [rcx], byte OpRet
    mov     rax, 1
    ret
endp

proc vmAdd
    mov     rax, rcx
    add     rax, rdx
    ret 
endp

proc WriteMessage uses rbx rsi rdi, message
    locals
        count   dd ?
    endl
    mov	    rdi,rcx                            ; first parameter passed in RCX
    invoke  GetStdHandle,STD_OUTPUT_HANDLE
    mov     rbx,rax
    xor     al,al
    or      rcx,-1
    repne	scasb
    dec     rdi
    mov     r8,-2
    sub     r8,rcx
    sub     rdi,r8
    invoke	WriteFile,rbx,rdi,r8,addr count,0
    ret
endp

section '.data' data readable writeable

StackTop:
    dq StackEnd
StackStart:
    dq 1024 dup 0
StackEnd:

section '.bss' data readable writeable

section '.edata' data readable

    data export
        export 'vm.dll',\
            vmAdd, 'Add',\
            EmitNop, 'EmitNop',\
            EmitI8, 'EmitI8',\
            EmitI16, 'EmitI16',\
            EmitI32, 'EmitI32',\
            EmitI64, 'EmitI64',\
            EmitAdd, 'EmitAdd',\
            EmitRet, 'EmitRet',\
            Execute, 'Execute',\
            GetStackAt, 'GetStackAt',\
            WriteMessage,'WriteMessage'
    end data

    data fixups
    end data

section '.idata' import data readable writeable

library kernel32,'kernel32.dll'

include 'api/kernel32.inc'
