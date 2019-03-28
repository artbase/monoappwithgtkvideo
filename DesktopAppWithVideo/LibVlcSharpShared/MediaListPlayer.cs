using System;
using System.Runtime.InteropServices;
using LibVLCSharp.Shared;

namespace DesktopAppWithVideo.LibVlcSharpShared
{
    public class MediaListPlayer : Internal
    {
        readonly struct Native
        {
            [DllImport(
                Constants.LibraryName,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_media_list_player_new"
            )]
            internal static extern IntPtr LibVLCMediaListPlayerNew(IntPtr libvlc);

            [DllImport(
                Constants.LibraryName,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_media_list_player_release"
            )]
            internal static extern void LibVLCMediaListPlayerRelease(IntPtr mediaListPlayer);

            [DllImport(
                Constants.LibraryName,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_media_list_player_set_media_list"
            )]
            internal static extern void LibVLCMediaListPlayerSetMediaList(IntPtr mediaListPlayer, IntPtr mediaList);

            [DllImport(
                Constants.LibraryName,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_media_list_player_set_media_player"
            )]
            internal static extern void LibVLCMediaListPlayerSetMediaPlayer(IntPtr mediaListPlayer, IntPtr mediaPlayer);
            [DllImport(
                Constants.LibraryName,
                CallingConvention = CallingConvention.Cdecl,
                EntryPoint = "libvlc_media_list_player_play"
            )]
            internal static extern void LibVLCMediaListPlayerPlay(IntPtr mediaListPlayer);

        }

        public MediaListPlayer(LibVLC libVLC)
            : base(() => Native.LibVLCMediaListPlayerNew(libVLC.NativeReference),
                        Native.LibVLCMediaListPlayerRelease)
        {

        }

        public void SetMediaList(MediaList mediaList)
        {
            Native.LibVLCMediaListPlayerSetMediaList(NativeReference, mediaList?.NativeReference ?? IntPtr.Zero);
        }

        public void SetMediaPlayer(MediaPlayer mediaPlayer)
        {
            Native.LibVLCMediaListPlayerSetMediaPlayer(NativeReference, mediaPlayer?.NativeReference ?? IntPtr.Zero);
        }

        public void Play()
        {
            Native.LibVLCMediaListPlayerPlay(NativeReference);
        }
    }
}
