      for i in 0..<100 {
        let greedy = try mcc.changeGreedy(i)
        let dynamic = try mcc.changeDynamic(i)
        
        XCTAssertEqual(greedy.reduce(0, +), dynamic.reduce(0, +), "Greedy and Dynamic return two different changes")
        
        if greedy.count != dynamic.count {
          print("\(i): greedy = \(greedy) dynamic = \(dynamic)")
        }
      }
