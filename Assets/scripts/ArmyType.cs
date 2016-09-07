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
	FighterAzor,
	BomberQuebrantahuesos,

	// Nazi units, ids starting in 100
	NaziJager=100,
	NaziFestung=101,
	NaziMeBf109=102
}