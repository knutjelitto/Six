for z in zeros[1...] {
    XCTAssertEqual(zeros[0], z)
    XCTAssertEqual(zeros[0].hashValue, z.hashValue)
}
