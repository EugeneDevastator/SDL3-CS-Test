using SDL;
using static SDL.SDL3;

class Program
{
    static int Main(string[] args)
    {
        unsafe
        {
            int width = 320,
                height = 240;
            bool loopShouldStop = false;

            SDL_Init((SDL_InitFlags)SDL_INIT_VIDEO);

            var win = SDL_CreateWindow("Hello World", width, height, 0);

            var renderer = SDL_CreateRenderer(win,(byte*)null);

            var bitmapSurface = SDL_LoadBMP("C:\\Eugene\\ctron.bmp");
            var bitmapTex = SDL_CreateTextureFromSurface(renderer, bitmapSurface);
            SDL_DestroySurface(bitmapSurface);

            while (!loopShouldStop)
            {
                unsafe
                {
                    SDL_Event evt;
                    while (SDL_PollEvent(&evt))
                    {
                        switch (evt.type)
                        {
                            case (uint)SDL_EventType.SDL_EVENT_QUIT:
                                loopShouldStop = true;
                                break;
                        }
                    }

                    SDL_RenderClear(renderer);
                    SDL_RenderTexture(renderer, bitmapTex, (SDL_FRect*)IntPtr.Zero, (SDL_FRect*)IntPtr.Zero);
                    SDL_RenderPresent(renderer);
                }
            }

            SDL_DestroyTexture(bitmapTex);
            SDL_DestroyRenderer(renderer);
            SDL_DestroyWindow(win);

            SDL_Quit();

            return 0;
        }
    }
}