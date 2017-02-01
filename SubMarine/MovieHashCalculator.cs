///<summary>
/// This class calculates the MovieHash for given movie file
/// This algorithm is taken from http://trac.opensubtitles.org/projects/opensubtitles/wiki/HashSourceCodes
///</summary> 

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SubMarine
{
    public class MovieHashCalculator
    {
        public long Lhash, Streamsize;

        public string SelectFile(string initialDirectory)
        {
            var dialog = new OpenFileDialog
            {
                Filter =
                    "Video files ( *.avi; *.divx; *.flv; *.m4v; *.mkv; *.mov; *.movie; *.mp4; *.mpeg; *.mpg; *.video; *.vob; *.wmv; *.x264; *.xvid)| *.avi; *.divx; *.flv; *.m4v; *.mkv; *.mov; *.movie; *.mp4; *.mpeg; *.mpg; *.video; *.vob; *.wmv; *.x264; *.xvid|All files (*.*)|*.*",
                InitialDirectory = initialDirectory,
                Title = "Select a video file"
            };
            return (dialog.ShowDialog() == DialogResult.OK)
                       ? dialog.FileName
                       : null;
        }

        public byte[] ComputeMovieHash(string filename)
        {
            byte[] result;
            using (Stream input = File.OpenRead(filename))
            {
                result = ComputeMovieHash(input);
            }
            return result;
        }

        public byte[] ComputeMovieHash(Stream input)
        {
            Streamsize = input.Length;
            Lhash = Streamsize;

            long i = 0;
            byte[] buffer = new byte[sizeof(long)];
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                Lhash += BitConverter.ToInt64(buffer, 0);
            }

            input.Position = Math.Max(0, Streamsize - 65536);
            i = 0;
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                Lhash += BitConverter.ToInt64(buffer, 0);
            }
            input.Close();
            byte[] result = BitConverter.GetBytes(Lhash);
            Array.Reverse(result);
            return result;
        }

        public string ToHexadecimal(byte[] bytes)
        {
            var hexBuilder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                hexBuilder.Append(i.ToString("x2"));
            }
            return hexBuilder.ToString();
        }

    }
}
