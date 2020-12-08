using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AnimEffectApp
{
	internal class MATRIXF
	{
		public const int FIXED_SHIFT = 16;
		public const int fixed1 = 1 << FIXED_SHIFT;


		//fields init MATRIXF matrix1 = { fixed1, 0, 0, 0, fixed1, 0, 0, 0, 1 };
		double fM11 = fixed1;
		double fM12 = 0;
		int iM13 = 0;
		double fM21 = 0;
		double fM22 = fixed1;
		int iM23 = 0;
		int iM31 = 0;
		int iM32 = 0;
		int iM33 = 1;

		public static MATRIXF matrix1 = new MATRIXF();

		public MATRIXF Clone()
		{
			MATRIXF mtRes = new MATRIXF();

			mtRes.fM11 = fM11;
			mtRes.fM12 = fM12;
			mtRes.iM13 = iM13;
			mtRes.fM21 = fM21;
			mtRes.fM22 = fM22;
			mtRes.iM23 = iM23;
			mtRes.iM31 = iM31;
			mtRes.iM32 = iM32;
			mtRes.iM33 = iM33;

			return mtRes;
		}//end Clone

		//////////////////////////////////////////////////////////////////////
		// Supp. functions
		//////////////////////////////////////////////////////////////////////
		public static int MulDiv(double dMultiplicand, double fMultiplier, int div)
		{
			return (int)(dMultiplicand * fMultiplier / (double)div);
		}//end MulDiv

		public static int MulDiv(int iMultiplicand, double fMultiplier, int div)
		{
			return (int)((double)iMultiplicand * fMultiplier / (double)div);
		}//end MulDiv

		public static int fixedMul(int iMultiplicand, double fMultiplier)
		{
			// iMultiplicand * fMultiplier / 65536
			return MulDiv(iMultiplicand, fMultiplier, 65536);
		}//end fixedMul

		public static int fixedMul(double dMultiplicand, double fMultiplier)
		{
			// iMultiplicand * fMultiplier / 65536
			return MulDiv(dMultiplicand, fMultiplier, 65536);
		}//end fixedMul

		public static double fixedDiv(int iNumerator, int iDenominator)
		{
			if (iNumerator == 0 || iDenominator == 0)
				return 0;

			// 65536 * iNumerator / iDenominator
			return MulDiv(65536, iNumerator, iDenominator);
		}//end fixedDiv

		public static Point operator *(Point point, MATRIXF matrix)
		{
			Point ptResult = new Point();
			ptResult.X = fixedMul(point.X, matrix.fM11) + fixedMul(point.Y, matrix.fM21) + matrix.iM31;
			ptResult.Y = fixedMul(point.X, matrix.fM12) + fixedMul(point.Y, matrix.fM22) + matrix.iM32;
			return ptResult;
		}//end operator *

		public static MATRIXF operator *(MATRIXF m1, MATRIXF m2)
		{
			MATRIXF mtRes = new MATRIXF();

			mtRes.fM11 = fixedMul(m1.fM11, m2.fM11) + fixedMul(m1.fM12, m2.fM21);
			mtRes.fM12 = fixedMul(m1.fM11, m2.fM12) + fixedMul(m1.fM12, m2.fM22);
			mtRes.iM13 = 0;
			mtRes.fM21 = fixedMul(m1.fM21, m2.fM11) + fixedMul(m1.fM22, m2.fM21);
			mtRes.fM22 = fixedMul(m1.fM21, m2.fM12) + fixedMul(m1.fM22, m2.fM22);
			mtRes.iM23 = 0;
			mtRes.iM31 = fixedMul(m1.iM31, m2.fM11) + fixedMul(m1.iM32, m2.fM21) + m2.iM31;
			mtRes.iM32 = fixedMul(m1.iM31, m2.fM12) + fixedMul(m1.iM32, m2.fM22) + m2.iM32;
			mtRes.iM33 = 1;

			return mtRes;
		}//end operator *

		public static MATRIXF mix(MATRIXF matrix1, MATRIXF matrix2, double fMix)
		{
			MATRIXF mtRes = new MATRIXF();

			mtRes.fM11 = fixedMul(matrix1.fM11, fMix) + fixedMul(matrix2.fM11, fixed1 - fMix);
			mtRes.fM12 = fixedMul(matrix1.fM12, fMix) + fixedMul(matrix2.fM12, fixed1 - fMix);
			mtRes.iM13 = fixedMul(matrix1.iM13, fMix) + fixedMul(matrix2.iM13, fixed1 - fMix);
			mtRes.fM21 = fixedMul(matrix1.fM21, fMix) + fixedMul(matrix2.fM21, fixed1 - fMix);
			mtRes.fM22 = fixedMul(matrix1.fM22, fMix) + fixedMul(matrix2.fM22, fixed1 - fMix);
			mtRes.iM23 = fixedMul(matrix1.iM23, fMix) + fixedMul(matrix2.iM23, fixed1 - fMix);
			mtRes.iM31 = fixedMul(matrix1.iM31, fMix) + fixedMul(matrix2.iM31, fixed1 - fMix);
			mtRes.iM32 = fixedMul(matrix1.iM32, fMix) + fixedMul(matrix2.iM32, fixed1 - fMix);
			mtRes.iM33 = fixedMul(matrix1.iM33, fMix) + fixedMul(matrix2.iM33, fixed1 - fMix);

			return mtRes;
		}

		public static Point mix(Point point1, Point point2, double fMix)
		{
			Point ptRes = new Point();
			ptRes.X = fixedMul(point1.X, fMix) + fixedMul(point2.X, fixed1 - fMix);
			ptRes.Y = fixedMul(point1.Y, fMix) + fixedMul(point2.Y, fixed1 - fMix);
			return ptRes;
		}

		public static Point sum(Point point1, Point point2)
		{
			Point ptRes = new Point();
			ptRes.X = point1.X + point2.X;
			ptRes.Y = point1.Y + point2.Y;
			return ptRes;
		}//end sum

		public static Point subtract(Point point1, Point point2)
		{
			Point ptRes = new Point();
			ptRes.X = point1.X - point2.X;
			ptRes.Y = point1.Y - point2.Y;
			return ptRes;
		}//end subtract

		public static MATRIXF offsetMatrix(int offsetX, int offsetY)
		{
			MATRIXF mRes = matrix1.Clone();
			mRes.iM31 = offsetX;
			mRes.iM32 = offsetY;
			return mRes;
		}

		public static MATRIXF scaleMatrix(double scaleX, double scaleY)
		{
			Point ptOrg = new Point();
			MATRIXF mRes = matrix1.Clone();
			mRes.fM11 = scaleX;
			mRes.fM22 = scaleY;
			return offsetMatrix(-ptOrg.X, -ptOrg.X) * mRes * offsetMatrix(ptOrg.X, ptOrg.X);
		}

		public static MATRIXF scaleMatrix(double scaleX, double scaleY, Point ptOrg)
		{
			MATRIXF mRes = matrix1.Clone();
			mRes.fM11 = scaleX;
			mRes.fM22 = scaleY;
			return offsetMatrix(-ptOrg.X, -ptOrg.X) * mRes * offsetMatrix(ptOrg.X, ptOrg.X);
		}

		public static MATRIXF rotateMatrix(double angle)
		{
			return rotateMatrix(angle, new Point());
		}//end rotateMatrix

		public static MATRIXF rotateMatrix(double angle, Point ptOrg)
		{
			MATRIXF mRes = matrix1.Clone();

			double dAngle = (angle / 65536.0) * 3.141592654 / 180.0;
			double fCos = (double)(65536.0 * Math.Cos(dAngle));
			double fSin = (double)(65536.0 * Math.Sin(dAngle));

			mRes.fM11 = fCos;
			mRes.fM21 = -fSin;
			mRes.fM12 = fSin;
			mRes.fM22 = fCos;

			return offsetMatrix(-ptOrg.X, -ptOrg.X) * mRes * offsetMatrix(ptOrg.X, ptOrg.X);
		}//end rotateMatrix
	}//end class MATRIXF

	internal struct AnimData
	{
		public AnimUtil.AnimOperation animType;

		public bool bOpen;

		public Effect effect;
		public Rectangle rcWnd;
		public Color color;

		public Point ptCenter;
		public Point ptRelRightTop;

		public int iAfterimages;
		public int iStep;
		public int iTotalSteps;
		public int iParameter;
	}//end class AnimData

	internal interface Effect
	{
		object Buffer {set; get;}
		bool Show(AnimData AD);
	}//end interface Effect

	public class AnimEffect
	{
		public Action<double> ReportProgressAction = (p) => { };

		public AnimEffect(int delay = 30, int steps = 50, int afterImages = 6)
		{
			Defaults(delay, steps, afterImages);
		}//end constructor

		public enum EffectType
		{
			Random			= -1,
			Spin			= 0,
			Vortex			= 1,
			ScatterGather	= 2,
			Spike			= 3,
			Fireworks		= 4
		}//end enum EffectType

		int m_iParameter;
		int m_iAfterimages;
		int m_iTotalSteps;
		int m_iDelay;

		EffectType m_EffectType;
		//Rectangle m_rcScreen;

		public void Defaults(int delay = 30, int steps = 50, int afterImages = 6)
		{
			//Size szMax = System.Windows.Forms.SystemInformation.MaxWindowTrackSize;
			//m_rcScreen = new Rectangle(0, 0, szMax.Width, szMax.Height);
			
			m_iAfterimages	= afterImages;
			m_iTotalSteps	= steps;
			m_iDelay		= delay;
			
			SetEffect(EffectType.Random);
		}//end Defaults

		public void Setup(int iSteps, int iAfterimages, int iDelay)
		{
			if (iSteps > 255) iSteps = 255;
			else if (iSteps < 1) iSteps = 1;
			m_iTotalSteps = iSteps;

			if (iAfterimages > 32) iAfterimages = 32;
			else if (iAfterimages < 0) iAfterimages = 0;
			m_iAfterimages = iAfterimages;

			if (iDelay > 100) iDelay = 100;
			else if (iDelay < 0) iDelay = 0;
			m_iDelay = iDelay;
		}//end Setup

		public void SetEffect(EffectType effect)
		{
			switch (effect)
			{
				case EffectType.Random:
					m_iParameter = 4;
					break;
				case EffectType.Spin:
					m_iParameter = 360;
					break;
				case EffectType.Vortex:
					m_iParameter = 180;
					break;
				case EffectType.ScatterGather:
					m_iParameter = 4;
					break;
				case EffectType.Spike:
					m_iParameter = 180;
					break;
				case EffectType.Fireworks:
					m_iParameter = 360;
					break;
			}//end switch

			m_EffectType = effect;
		}//end SetEffect

		internal Effect ChooseFunc()
		{
			bool bRandom = false;
			if (m_EffectType == EffectType.Random)
			{
				bRandom = true;
				SetEffect((EffectType)(new System.Random().Next(0,4)));
			}//end if

			Effect effect = null;
			switch (m_EffectType)
			{
				case EffectType.Spin:
					effect = new AnimUtil.efSpinFrame();
					break;
				case EffectType.Vortex:
					effect = new AnimUtil.efVortexFrames();
					break;
				case EffectType.ScatterGather:
					effect = new AnimUtil.efScatterGatherFrames();
					break;
				case EffectType.Spike:
					effect = new AnimUtil.efSpikeFrames();
					break;
				case EffectType.Fireworks:
					effect = new AnimUtil.efFireworxFrames();
					break;
				default:
					effect = null;
					break;
			}//end switch

			if (bRandom) //restore random effect type - if was
				m_EffectType = EffectType.Random;

			return effect;
		}//end ChooseFunc

		//
		public void Play(Rectangle rcWnd, Color color, bool bOpen)
		{
			AnimData ad = new AnimData();

			ad.effect			= ChooseFunc();

			ad.bOpen			= bOpen;
			ad.iAfterimages		= m_iAfterimages;
			ad.iTotalSteps		= m_iTotalSteps;
			ad.iParameter		= m_iParameter;
			ad.rcWnd			= rcWnd;
			ad.color			= color;
			ad.ptCenter			= AnimUtil.CenterPoint(ad.rcWnd);
			ad.ptRelRightTop	= new Point(rcWnd.Right - ad.ptCenter.X, rcWnd.Top - ad.ptCenter.Y);
			
			Animate(ad);
		}//end Play

		private void Animate(AnimData animData)
		{
			animData.animType = AnimUtil.AnimOperation.AnimInit;
			animData.effect.Show(animData);

			int nTotalFrames = animData.iTotalSteps + animData.iAfterimages;

			if (animData.bOpen)
			{
				for (int frame = 0; frame < nTotalFrames; frame++)
				{
					// draw 
					if (frame < animData.iTotalSteps)
					{
						animData.animType = AnimUtil.AnimOperation.AnimDraw;
						animData.iStep = frame;
						animData.effect.Show(animData);
					}//end if

					System.Threading.Thread.Sleep(m_iDelay);

					// erase
					if (frame >= animData.iAfterimages)
					{
						animData.animType = AnimUtil.AnimOperation.AnimErase;
						animData.iStep = frame - animData.iAfterimages;
						animData.effect.Show(animData);
					}//end if

					ReportProgressAction(frame/(double)nTotalFrames);
				}//end for
			}//end if
			else //Close
			{
				for (int frame = nTotalFrames - 1; frame >= 0; frame--)
				{
					// draw 
					if (frame >= animData.iAfterimages)
					{
						animData.animType = AnimUtil.AnimOperation.AnimDraw;
						animData.iStep = frame - animData.iAfterimages;
						animData.effect.Show(animData);
					}//end if

					System.Threading.Thread.Sleep(m_iDelay);

					// erase
					if (frame < animData.iTotalSteps)
					{
						animData.animType = AnimUtil.AnimOperation.AnimErase;
						animData.iStep = frame;
						animData.effect.Show(animData);
					}//end if

					ReportProgressAction(frame / (double)nTotalFrames);
				}//end for
			}//end else 

			animData.animType = AnimUtil.AnimOperation.AnimTerm;

			animData.effect.Show(animData);
		}//end Animate
	}//end class AnimEffect

	internal class AnimUtil
	{
		public enum AnimOperation
		{
			AnimDraw,
			AnimErase,
			AnimInit,
			AnimTerm
		}//end enum AnimOperation

		/////////////////////////////////////////////////////////
		// Effects
		/////////////////////////////////////////////////////////

		internal class efSpinFrame : Effect
		{
			public object Buffer { get { return null; } set {} }

			public bool Show(AnimData AD)
			{
				if (AD.animType == AnimOperation.AnimDraw || AD.animType == AnimOperation.AnimErase)
				{
					Point[] ptRect = new Point[4];
					
					ptRect[0] = AD.ptRelRightTop;
					ptRect[1].X = AD.ptRelRightTop.X;
					ptRect[1].Y = -AD.ptRelRightTop.Y;

					int nRemainSteps = AD.iStep - AD.iTotalSteps;
					int nRemainAngle = AD.iParameter * nRemainSteps;

					double fxScale = MATRIXF.fixedDiv(AD.iStep, AD.iTotalSteps);
					double fxAngle = MATRIXF.fixedDiv(nRemainAngle, AD.iTotalSteps);


					MATRIXF matrix = MATRIXF.scaleMatrix(fxScale, fxScale) * MATRIXF.rotateMatrix(fxAngle);
					ptRect[0] = ptRect[0] * matrix;
					ptRect[1] = ptRect[1] * matrix;
					ptRect[2] = MATRIXF.sum(AD.ptCenter, ptRect[0]);
					ptRect[3] = MATRIXF.sum(AD.ptCenter, ptRect[1]);
					ptRect[0] = MATRIXF.subtract(AD.ptCenter, ptRect[0]);
					ptRect[1] = MATRIXF.subtract(AD.ptCenter, ptRect[1]);

					drawPoly(ptRect, AD.color);
				}//end if

				return true;
			}//end Show
		}//end class efSpinFrame

		internal class efVortexFrames : Effect
		{
			private object oBuffer = null;
			public object Buffer { get { return oBuffer; } set { oBuffer = value; } }

			public bool Show(AnimData AD)
			{
				switch (AD.animType)
				{
					case AnimOperation.AnimInit:
						AD.effect.Buffer = MATRIXF.rotateMatrix(MATRIXF.fixed1 * 72, AD.ptCenter);
						break;
					case AnimOperation.AnimDraw:
					case AnimOperation.AnimErase:
						{
							Point ptBoxRel = new Point();

							ptBoxRel.X = AD.ptRelRightTop.X * AD.iStep / AD.iTotalSteps;
							ptBoxRel.Y = AD.ptRelRightTop.Y * AD.iStep / AD.iTotalSteps;

							MATRIXF matrix;
							double fxScale;

							fxScale = MATRIXF.fixedDiv((AD.iTotalSteps - AD.iStep) * 4, AD.iTotalSteps * 3);
							matrix = MATRIXF.offsetMatrix(AD.ptRelRightTop.X, AD.ptRelRightTop.Y) *
								MATRIXF.scaleMatrix(fxScale, fxScale, AD.ptCenter) *
								MATRIXF.rotateMatrix(MATRIXF.fixedDiv(AD.iParameter * AD.iStep, AD.iTotalSteps), AD.ptCenter);

							Point ptBoxCenter;
							ptBoxCenter = AD.ptCenter * matrix;

							for (int iLoop = 0; iLoop < 5; iLoop++)
							{
								drawBox(ptBoxCenter, ptBoxRel, AD.color);
								ptBoxCenter = ptBoxCenter * (MATRIXF)AD.effect.Buffer;
							}//end for

							break;
						}//end case
				}//end switch

				return true;
			}//end Show
		}//end class efVortexFrames

		internal class efScatterGatherFrames : Effect
		{
			public object Buffer { get { return null; } set { } }

			public bool Show(AnimData AD)
			{
				if (AD.animType == AnimOperation.AnimDraw || AD.animType == AnimOperation.AnimErase)
				{
					int iDivisor = AD.iParameter;
					Point ptBoxRel = new Point();
					ptBoxRel.X = AD.ptRelRightTop.X * AD.iStep / (AD.iTotalSteps * iDivisor);
					ptBoxRel.Y = AD.ptRelRightTop.Y * AD.iStep / (AD.iTotalSteps * iDivisor);

					MATRIXF matrix;
					double fxScale;

					fxScale = MATRIXF.fixedDiv(AD.iTotalSteps * 3 - AD.iStep * 2, AD.iTotalSteps);
					matrix = MATRIXF.scaleMatrix(fxScale, fxScale) * MATRIXF.offsetMatrix(AD.ptCenter.X, AD.ptCenter.Y);

					for (int iRow = 0; iRow < iDivisor; iRow++)
					{
						for (int iCol = 0; iCol < iDivisor; iCol++)
						{
							Point ptTileCenter = new Point();

							ptTileCenter.X = (iRow * 2 - iDivisor + 1) * AD.ptRelRightTop.X / iDivisor;
							ptTileCenter.Y = (iCol * 2 - iDivisor + 1) * AD.ptRelRightTop.Y / iDivisor;
							ptTileCenter = ptTileCenter * matrix;

							drawBox(ptTileCenter, ptBoxRel, AD.color);
						}//end for
					}//end for
				}//end if

				return true;
			}//end Show
		}//end class efScatterGatherFrames

		internal class SpikeData
		{

			public Point[][] ptTriangleEnd = new Point[16][]; //[16][36]
			public Point[] ptEndCenter = InitArray(16);
			public Point[] ptTriangleCenter = InitArray(16);

			public MATRIXF[] matrixCircle = new MATRIXF[16];

			public SpikeData()
			{
				for (int i = 0; i < ptTriangleEnd.Length; i++)
					ptTriangleEnd[i] = InitArray(3);

				for (int i = 0; i < matrixCircle.Length; i++)
					matrixCircle[i] = new MATRIXF();
			}//end constructor

			private static Point[] InitArray(int count)
			{
				Point [] v = new Point[count];
				for (int i = 0; i < v.Length; i++)
					v[i] = new Point();
				return v;
			}//end InitArray
		}//end class SpikeData

		internal class efSpikeFrames : Effect
		{
			private object oBuffer = null;
			public efSpikeFrames()
			{
				oBuffer = new SpikeData();
			}//end constructor
			public object Buffer { get { return oBuffer; } set { oBuffer = value; } }

			public bool Show(AnimData AD)
			{
				SpikeData sd = ((SpikeData)AD.effect.Buffer);

				switch (AD.animType)
				{
					case AnimOperation.AnimInit:
						{
							int xLeft = AD.rcWnd.Left;
							int xRight = AD.rcWnd.Right;
							int yTop = AD.rcWnd.Bottom;
							int yBottom = AD.rcWnd.Top;

							for (int idx = 0; idx < 16; idx++)
							{

								Point[] pTriangle = sd.ptTriangleEnd[idx];

								pTriangle[0] = AD.ptCenter;

								if (idx < 4)
								{
									pTriangle[1].X = pTriangle[2].Y = yTop;
									pTriangle[1].X = (xLeft * (4 - idx) + xRight * idx) / 4;
									pTriangle[2].X = (xLeft * (3 - idx) + xRight * (idx + 1)) / 4;
								}
								else if (idx < 8)
								{
									pTriangle[1].X = pTriangle[2].X = xRight;
									pTriangle[1].Y = (yTop * (8 - idx) + yBottom * (idx - 4)) / 4;
									pTriangle[2].Y = (yTop * (7 - idx) + yBottom * (idx - 3)) / 4;
								}
								else if (idx < 12)
								{
									pTriangle[1].Y = pTriangle[2].Y = yBottom;
									pTriangle[1].X = (xRight * (12 - idx) + xLeft * (idx - 8)) / 4;
									pTriangle[2].X = (xRight * (11 - idx) + xLeft * (idx - 7)) / 4;
								}
								else
								{
									pTriangle[1].X = pTriangle[2].X = xLeft;
									pTriangle[1].Y = (yBottom * (16 - idx) + yTop * (idx - 12)) / 4;
									pTriangle[2].Y = (yBottom * (15 - idx) + yTop * (idx - 11)) / 4;
								}

								sd.ptEndCenter[idx].X = (pTriangle[0].X + pTriangle[1].X + pTriangle[2].X) / 3;
								sd.ptEndCenter[idx].Y = (pTriangle[0].Y + pTriangle[1].Y + pTriangle[2].Y) / 3;
							}

							Point ptTrgCenter = new Point();

							ptTrgCenter.X = AD.ptCenter.X;

							ptTrgCenter.Y = AD.ptCenter.Y + (AD.ptRelRightTop.X + AD.ptRelRightTop.Y) * 4 / 5;

							for (int idx = 0; idx < 16; idx++)
							{
								MATRIXF matrix;

								matrix = MATRIXF.rotateMatrix((33 * MATRIXF.fixed1) + (-22 * MATRIXF.fixed1) * idx, AD.ptCenter);
								sd.ptTriangleCenter[idx] = ptTrgCenter * matrix;
								Point ptTemp = MATRIXF.subtract(sd.ptTriangleCenter[idx], sd.ptEndCenter[idx]);
								sd.matrixCircle[idx] = MATRIXF.offsetMatrix(ptTemp.X, ptTemp.Y);
							}

							break;
						}//end case
					case AnimOperation.AnimDraw:
					case AnimOperation.AnimErase:
						{
							Point[] ptTriangle = new Point[3];

							double fixedFactor;
							MATRIXF matrix;
							double fxScale;

							fxScale = MATRIXF.fixedDiv(AD.iStep, AD.iTotalSteps);

							if (AD.iStep < AD.iTotalSteps / 2)
							{
								fixedFactor = (MATRIXF.fixed1 - MATRIXF.fixedDiv(AD.iStep * 2, AD.iTotalSteps)) * AD.iParameter;
								for (int idx = 0; idx < 16; idx++)
								{
									matrix = MATRIXF.scaleMatrix(fxScale, fxScale, sd.ptEndCenter[idx]) *
									MATRIXF.rotateMatrix(fixedFactor, sd.ptEndCenter[idx]);

									matrix = matrix * sd.matrixCircle[idx];

									for (int iAngle = 0; iAngle < 3; iAngle++)
									{
										ptTriangle[iAngle] = sd.ptTriangleEnd[idx][iAngle] * matrix;
									}

									drawPoly(ptTriangle, AD.color);
								}
							}
							else
							{
								fixedFactor = MATRIXF.fixedDiv(AD.iStep * 2 - AD.iTotalSteps, AD.iTotalSteps);
								for (int idx = 0; idx < 16; idx++)
								{
									matrix = MATRIXF.scaleMatrix(fxScale, fxScale, sd.ptEndCenter[idx]);
									matrix *= MATRIXF.mix(MATRIXF.matrix1, sd.matrixCircle[idx], fixedFactor);

									for (int iAngle = 0; iAngle < 3; iAngle++)
									{
										ptTriangle[iAngle] = sd.ptTriangleEnd[idx][iAngle] * matrix;
									}

									drawPoly(ptTriangle, AD.color);
								}
							}
							break;
						}//end case
				}//end switch

				return true;
			}//end show
		}//end class efSpikeFrames

		internal class FWData
		{
			public const int NRECT = 10;
			public Point[] ptCenter = new Point[NRECT];
		}//end class FWData

		internal class efFireworxFrames : Effect
		{
			private object oBuffer = null;
			public efFireworxFrames()
			{
				oBuffer = new FWData();
			}//end constructor
			public object Buffer { get { return oBuffer; } set { oBuffer = value; } }

			public bool Show(AnimData AD)
			{
				FWData fw = ((FWData)AD.effect.Buffer);

				switch (AD.animType)
				{
					case AnimOperation.AnimInit:
						{
							Point[] ptCenter = fw.ptCenter;
							Point ptRectCenter = new Point();


							ptRectCenter.X = AD.ptCenter.X;
							ptRectCenter.Y = AD.ptCenter.Y + (AD.ptRelRightTop.X + AD.ptRelRightTop.Y) * 5 / 3;

							for (int idx = 0; idx < FWData.NRECT; idx++)
							{
								MATRIXF matrix = MATRIXF.rotateMatrix(idx * (360 * MATRIXF.fixed1 / FWData.NRECT), AD.ptCenter);
								ptCenter[idx] = ptRectCenter * matrix;
							}//end for

							break;
						}//end case
					case AnimOperation.AnimDraw:
					case AnimOperation.AnimErase:
						{
							Point ptTemp = new Point();

							double fixedFactor = MATRIXF.fixedDiv(AD.iStep, AD.iTotalSteps);

							MATRIXF matrix;

							Point[] ptRect = new Point[4];
							Point[] ptTmp = new Point[4];

							ptRect[0].X = ptRect[3].X = -AD.ptRelRightTop.X;
							ptRect[1].X = ptRect[2].X = AD.ptRelRightTop.X;
							ptRect[0].Y = ptRect[1].Y = AD.ptRelRightTop.Y;
							ptRect[2].Y = ptRect[3].Y = -AD.ptRelRightTop.Y;

							for (int idx = 0; idx < FWData.NRECT; idx++)
							{
								matrix = MATRIXF.scaleMatrix(fixedFactor, fixedFactor) *
								MATRIXF.rotateMatrix((MATRIXF.fixed1 - fixedFactor) * AD.iParameter);

								ptTemp = MATRIXF.mix(AD.ptCenter, fw.ptCenter[idx], fixedFactor);
								matrix = matrix * MATRIXF.offsetMatrix(ptTemp.X, ptTemp.Y);

								for (int iAngle = 0; iAngle < 4; iAngle++)
									ptTmp[iAngle] = ptRect[iAngle] * matrix;

								drawPoly(ptTmp, AD.color);
							}//end for

							break;
						}//end case
				}//end switch

				return true;
			}//end Show
		}//end class efFireworxFrames

		/////////////////////////////////////////////////////////

		// Primitives

		/////////////////////////////////////////////////////////

		internal static Point CenterPoint(Rectangle rc)
		{
			int x = rc.Left + rc.Width / 2;
			int y = rc.Top + rc.Height / 2;
			return new Point(x, y);
		}//end CenterPoint

		internal static void drawBox(Point ptCenter, Point ptRelRightTop, Color color)
		{

			if (ptRelRightTop.X == 0 && ptRelRightTop.Y == 0)
				return;

			Point[] ptTemp = new Point[4];

			ptTemp[0].X = ptCenter.X - ptRelRightTop.X;
			ptTemp[0].Y = ptCenter.Y + ptRelRightTop.Y;
			
			ptTemp[1].X = ptCenter.X + ptRelRightTop.X;
			ptTemp[1].Y = ptCenter.Y + ptRelRightTop.Y;
			
			ptTemp[2].X = ptCenter.X + ptRelRightTop.X;
			ptTemp[2].Y = ptCenter.Y - ptRelRightTop.Y;
			
			ptTemp[3].X = ptCenter.X - ptRelRightTop.X;
			ptTemp[3].Y = ptCenter.Y - ptRelRightTop.Y;

			DrawPolygon(ptTemp, color);
		}//end drawBox

		internal static void drawPoly(Point[] pPts, Color color)
		{
			int dwPoints = pPts.Length;
			if (pPts == null || dwPoints == 0)
				return;

			if (pPts[dwPoints - 1].X == pPts[0].X && pPts[dwPoints - 1].Y == pPts[0].Y)
				return;

			DrawPolygon(pPts, color);
		}//end drawPoly

		internal static void DrawPolygon(Point[] pPts, Color color)
		{
			//Task t = Task.Factory.StartNew(() =>
			//{
				Point pt = pPts[0];
				for (int i = 1; i < pPts.Length; i++)
				{
					ControlPaint.DrawReversibleLine(pt, pPts[i], color);
					pt = pPts[i];
				}//end for
				ControlPaint.DrawReversibleLine(pt, pPts[0], color);
			//});
		}//end DrawPolygon
	}//end class AnimUtil
}//end namespace AnimEffectApp

