package main

func TestNegativeRead(t *testing.T) {
    defer func() {
        switch err := recover().(type) {
        case nil:
            t.Fatal("read did not panic")
        }
    }()
}
