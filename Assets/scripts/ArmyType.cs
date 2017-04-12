using System;

[System.Flags]
public enum ArmyType
{
	Unknown=-1,
	Empty=0,
	Milicia,
	TankLince,
	TankToro,
	Antiaereo,
	TankBisonte,
	FighterAzor=50,				// Air force units id > 50
	BomberQuebrantahuesos,
	FighterBomberHalcon,

	// Nazi units, ids starting in 100
	NaziTroop=100,
	NaziTiger=101,
	NaziPanzer1=102
}