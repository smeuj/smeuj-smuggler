using CommandLine;

namespace SmeujSmuggler {

    internal class Options {
        [Option('i', "identity", Required = false,
            HelpText = "Specifies the path to the identity file.")]
        public string? IdentityFilePath { get; set; }

        [Option('s', "smeuj", Required = true,
            HelpText = "Specifies the path to the file containing the smeuj.")]
        public string? SmeujFilePath { get; set; }
    }

}
