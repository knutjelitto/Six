class Source
{
    let name: string
    let content: string

    init(_ name: string, _ content: string)
    {
        self.name = name
        self.content = content
    }
}

class SourceIndex
{
    let Index: Array<int>
    let Source: Source

    init(_ source: Source)
    {
        Source = source
        Index = BuildIndex()
    }

    static func BuildIndex()
    {

    }
}
