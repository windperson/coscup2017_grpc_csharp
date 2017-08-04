using CommandLine;

namespace coscup2017_grpc_csharp
{
    class Options
    {
        [Value(0)]
        public string HostAddr {get; set;}

        [Value(1)]
        public string DocPath {get; set;}

    }
}