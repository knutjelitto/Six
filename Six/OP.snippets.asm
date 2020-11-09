;;== Nop,0
    ret

;;== I8,1
    movsx   rax, byte [rsi]
    add     rsi, 1
    sub     rdi, 8
    mov     [rdi], rax
    ret

;;== I16,2
    movsx   rax, word [rsi]
    add     rsi, 2
    sub     rdi, 8
    mov     [rdi], rax
    ret

;;== I32,4
    movsxd  rax, dword [rsi]
    add     rsi, 4
    sub     rdi, 8
    mov     [rdi], rax
    ret

;;== I64,8
    mov     rax, qword [rsi]
    add     rsi, 8
    sub     rdi, 8
    mov     [rdi], rax
    ret

;;== Add,0
    mov     rax, [rdi]
    add     rax, [rdi + 8]
    add     rdi, 8
    mov     [rdi], rax
    ret

;;== Ret,0
    pop     rax
    mov     rax, 42
    jmp     Return


