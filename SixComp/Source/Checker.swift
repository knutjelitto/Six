private final class JSONDumper: DependenciesDumper {
    func dump(dependenciesOf rootpkg: ResolvedPackage, on stream: OutputByteStream) {
        func convert(_ package: ResolvedPackage) -> JSON {
            return .orderedDictionary([
                "name": .string(package.name),
                "url": .string(package.manifest.url),
                "version": .string(package.manifest.version?.description ?? "unspecified"),
                "path": .string(package.path.pathString),
                "dependencies": .array(package.dependencies.map(convert)),
            ])
        }

        stream <<< convert(rootpkg).toString(prettyPrint: true) <<< "\n"
    }
}

public enum ShowDependenciesMode: String, RawRepresentable, CustomStringConvertible {
    case text, dot, json, flatlist

    public init?(rawValue: String) {
        switch rawValue.lowercased() {
        case "text":
           self = .text
        case "dot":
           self = .dot
        case "json":
           self = .json
        case "flatlist":
            self = .flatlist
        default:
            return nil
        }
    }

    public var description: String {
        switch self {
        case .text: return "text"
        case .dot: return "dot"
        case .json: return "json"
        case .flatlist: return "flatlist"
        }
    }
}
