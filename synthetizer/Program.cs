using synthetizer;

WAVBuilder builder = new WAVBuilder("../test.wav");
builder.Write(Signal.SineSignal(4400, 1, 48, 5));
