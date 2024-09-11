using synthetizer;

WAVBuilder builder = new WAVBuilder("../test.wav");
builder.Write(Signal.SquareSignal(20, 1, 48, 5));
