using System;

namespace SmeujSmuggler {

    internal record Smeu(
        string   Author,
        string?  Inspiration,
        DateTime Time,
        string   Content,
        string?  Example
     );

}
