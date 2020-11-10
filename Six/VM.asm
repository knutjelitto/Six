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
;; R8       -- var  -- Param 3      var
;; R9       -- var  -- Param 4      var
;; R10      -- var                  var
;; R11      -- var                  var
;; R12      -- fix
;; R13      -- fix
;; R14      -- fix
;; R15      -- fix
;;
;; XMM0     -- var  -- Param 1      var
;; XMM1     -- var  -- Param 2      var
;; XMM2     -- var  -- Param 3      var
;; XMM3     -- var  -- Param 4      var
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

InnerExec:
    movzx   rax, byte [rsi]
    inc     rsi
    call    qword [Operations + rax*8]
    jmp     InnerExec
Return:
    ret

; (index) -> (value)
proc GetStackAt index
    mov     rax, [StackTop]
    mov     rax, [rax + rcx*8]
    ret
endp

; () -> (builder)
proc CreateBuilder
    mov     rax, BuildStart
    ret
endp

; (builder) -> ()
proc DestroyBuilder
    ret
endp

Operations:
    dq      ExecNop
    dq      ExecI8
    dq      ExecI16
    dq      ExecI32
    dq      ExecI64
    dq      ExecAdd
    dq      ExecRet

    OpNop = 0
    OpI8 = 1
    OpI16 = 2
    OpI32 = 3
    OpI64 = 4
    OpAdd = 5
    OpRet = 6

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
    ret

ExecRet:
    pop     rax
    mov     rax, 42
    jmp     Return


EmitNop:
    mov     [rcx], byte OpNop
    lea     rax, [rcx + 1]
    ret

EmitI8:
    mov     [rcx], byte OpI8
    mov     [rcx + 1], dl
    lea     rax, [rcx + 2]
    ret

EmitI16:
    mov     [rcx], byte OpI16
    mov     [rcx + 1], dx
    lea     rax, [rcx + 3]
    ret

EmitI32:
    mov     [rcx], byte OpI32
    mov     [rcx + 1], edx
    lea     rax, [rcx + 5]
    ret

EmitI64:
    mov     [rcx], byte OpI64
    mov     [rcx + 1], rdx
    lea     rax, [rcx + 9]
    ret

EmitAdd:
    mov     [rcx], byte OpAdd
    lea     rax, [rcx + 1]
    ret

EmitRet:
    mov     [rcx], byte OpRet
    lea     rax, [rcx + 1]
    ret


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

BuildStart:
    dq 1024 dup 0
BuildEnd:

;;section '.bss' data readable writeable

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
            CreateBuilder, 'CreateBuilder',\
            DestroyBuilder, 'DestroyBuilder',\
            GetStackAt, 'GetStackAt',\
            WriteMessage,'WriteMessage'
    end data

    data fixups
    end data

section '.idata' import data readable writeable

library kernel32,'kernel32.dll'

include 'api/kernel32.inc'
