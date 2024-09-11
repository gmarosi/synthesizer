using synthetizer;

WAVBuilder builder = new WAVBuilder("../test.wav");
builder.Write(Signal.AddSignals(Signal.SquareSignal(440, 1, 48, 5), 1, Signal.SquareSignal(220, 1, 48, 5), 0.25f));
