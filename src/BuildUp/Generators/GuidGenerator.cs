using System;

namespace BuildUp.Generators
{
    /// <summary>
    /// Creates generators for Guids
    /// </summary>
	public static class GuidGenerator
	{
        /// <summary>
        /// Generates a sequence of Guids based on random numbers generated using the seed value.
        /// The sequence is deterministic, i.e., using the same seed will result in the same 
        /// sequence, making it useful where a predictable, known sequence is required.
        /// The values generated follow version 4 UUID format defined in http://www.ietf.org/rfc/rfc4122.txt
        /// <example>
        /// <para>
        /// 
        /// </para>
        /// </example>
        /// </summary>
        /// <param name="seed"> </param>
        /// <returns></returns>
        public static IGenerator<Guid> Incrementing(int seed)
        {
            /*
            http://www.ietf.org/rfc/rfc4122.txt
            
      The formal definition of the UUID string representation is
      provided by the following ABNF [7]:

      UUID                   = time-low "-" time-mid "-"
                               time-high-and-version "-"
                               clock-seq-and-reserved
                               clock-seq-low "-" node
      time-low               = 4hexOctet
      time-mid               = 2hexOctet
      time-high-and-version  = 2hexOctet
      clock-seq-and-reserved = hexOctet
      clock-seq-low          = hexOctet
      node                   = 6hexOctet
      hexOctet               = hexDigit hexDigit
      hexDigit =
            "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7" / "8" / "9" /
            "a" / "b" / "c" / "d" / "e" / "f" /
            "A" / "B" / "C" / "D" / "E" / "F"
             
   The version 4 UUID is meant for generating UUIDs from truly-random or
   pseudo-random numbers.

   The algorithm is as follows:

   o  Set the two most significant bits (bits 6 and 7) of the
      clock_seq_hi_and_reserved to zero and one, respectively.

   o  Set the four most significant bits (bits 12 through 15) of the
      time_hi_and_version field to the 4-bit version number from
      Section 4.1.3.

   o  Set all the other bits to randomly (or pseudo-randomly) chosen
      values.
             */

            return Generator.Create((random,index) =>
            {
                var nextBytes = new byte[16];
                random.NextBytes(nextBytes);

                // clock_seq_and_reserved
                nextBytes[9] |= (1 << 6);
                nextBytes[9] &= unchecked((byte)(~(1 << 7)));

                // time_hi_and_version
                nextBytes[7] &= unchecked((byte)(~(1 << 7)));
                nextBytes[7] |= (1 << 6);
                nextBytes[7] &= unchecked((byte)(~(1 << 5)));
                nextBytes[7] &= unchecked((byte)(~(1 << 4)));

                var next = new Guid(nextBytes);
                return next;
            }, () => new Random(seed));
        }


		
	}
}