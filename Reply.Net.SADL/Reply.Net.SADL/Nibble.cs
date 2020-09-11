namespace Reply.Net.SADL
{
  internal class Nibble
  {
    public byte Value { get; private set; }

    public Nibble()
    {
      Value = 0x00;
    }

    public Nibble(int value) : this((byte)value)
    {
    }

    public Nibble(byte value)
    {
      Value = (byte)(value & 0x0F);
    }

    public override string ToString()
    {
      return (Value).ToString("x2")[1].ToString();
    }
  }
}
