using System;
using System.Collections.Generic;

namespace SmeujSmuggler {

    internal record Smeu(
        string        Author,
        List<string>  Inspirations,
        DateTime      Time,
        string        Content,
        List<Example> Examples
     );

    internal record Example(
        string   Author,
        DateTime Time,
        string   Content
    );

}
