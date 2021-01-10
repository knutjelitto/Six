    if _slowPath(error) {
      _fatalErrorMessage("Fatal error", "Overflow/underflow", file: file, line: line, flags: _fatalErrorFlags())
    }
