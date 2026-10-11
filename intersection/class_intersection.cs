using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intersection
{
    public class kamyar_intersection
    {
		
		public int get_2Dline_intersection(Point3D Point0, Point3D Point1, Point3D Point2, Point3D Point3 , ref Point3D Point_intersection)

		{
			double p0_x, p0_y, p1_x, p1_y, p2_x, p2_y, p3_x, p3_y, i_x, i_y;
			if (Point0.Z == Point1.Z && Point0.Z == Point2.Z && Point0.Z == Point3.Z) // in XY Plane
            {
				//Point_intersection.Z = Point0.Z;
				p0_x = Point0.X; p0_y = Point0.Y;
				p1_x = Point1.X; p1_y = Point1.Y;
				p2_x = Point2.X; p2_y = Point2.Y;
				p3_x = Point3.X; p3_y = Point3.Y;
			}
			else if (Point0.X == Point1.X && Point0.X == Point2.X && Point0.X == Point3.X) // in ZY Plane
			{
				//Point_intersection.X = Point0.X;
				p0_x = Point0.Z; p0_y = Point0.Y;
				p1_x = Point1.Z; p1_y = Point1.Y;
				p2_x = Point2.Z; p2_y = Point2.Y;
				p3_x = Point3.Z; p3_y = Point3.Y;
			}
			else if (Point0.Y == Point1.Y && Point0.Y == Point2.Y && Point0.Y == Point3.Y) // in XZ Plane
			{
				//Point_intersection.Y = Point0.Y;
				p0_x = Point0.X; p0_y = Point0.Z;
				p1_x = Point1.X; p1_y = Point1.Z;
				p2_x = Point2.X; p2_y = Point2.Z;
				p3_x = Point3.X; p3_y = Point3.Z;
            }
            else
            {
				return 2; // Two lines are not in the same surface
            }

			/////continuous lines
			if ((Point0.X==Point2.X && Point0.Y == Point2.Y && Point0.Z == Point2.Z) || (Point0.X == Point3.X && Point0.Y == Point3.Y && Point0.Z == Point3.Z)) 
			{
				Point_intersection.X = Point0.X;
				Point_intersection.Y = Point0.Y;
				Point_intersection.Z = Point0.Z;
				return 1;
			}
			if ((Point1.X == Point2.X && Point1.Y == Point2.Y && Point1.Z == Point2.Z) || (Point1.X == Point3.X && Point1.Y == Point3.Y && Point1.Z == Point3.Z))
			{
				Point_intersection.X = Point1.X;
				Point_intersection.Y = Point1.Y;
				Point_intersection.Z = Point1.Z;
				return 1;
			}

			/////////////////////////////////////////////////
			///		//public int get_2Dline_intersection(float p0_x, float p0_y, float p1_x, float p1_y, float p2_x, float p2_y, float p3_x, float p3_y, ref float i_x, ref float i_y)
			double s02_x, s02_y, s10_x, s10_y, s32_x, s32_y, s_numer, t_numer, denom, t;
			s10_x = p1_x - p0_x;
			s10_y = p1_y - p0_y;
			s32_x = p3_x - p2_x;
			s32_y = p3_y - p2_y;

			denom = s10_x * s32_y - s32_x * s10_y;
			if (denom == 0F)
			{
				return 3; // Collinear
			}
			bool denomPositive = denom > 0F;

			s02_x = p0_x - p2_x;
			s02_y = p0_y - p2_y;
			//s_numer = s10_x * s02_y - s10_y * s02_x;
			//if ((s_numer < 0F) == denomPositive) 
			//{
			//	return 0; // No collision
			//}

			t_numer = s32_x * s02_y - s32_y * s02_x;
			//if ((t_numer < 0F) == denomPositive)
			//{
			//	return 0; // No collision
			//}

			//if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
			//{
			//	return 0; // No collision
			//}

			// Collision detected
			t = t_numer / denom;
			//if (i_x != null)
				i_x = p0_x + (t * s10_x);
			//if (i_y != null)
				i_y = p0_y + (t * s10_y);


			double tolerance = 1e-6;
			if (i_x > Math.Max(p0_x, p1_x) + tolerance || i_x < Math.Min(p0_x, p1_x)- tolerance || i_y > Math.Max(p0_y, p1_y)+ tolerance || i_y < Math.Min(p0_y, p1_y)- tolerance)
				return 0;
			if (i_x > Math.Max(p2_x, p3_x)+ tolerance || i_x < Math.Min(p2_x, p3_x)- tolerance || i_y > Math.Max(p2_y, p3_y)+ tolerance || i_y < Math.Min(p2_y, p3_y)- tolerance)
				return 0;
			//return 1;
			/////////////////////////////////////////////////
			if (Point0.Z == Point1.Z && Point0.Z == Point2.Z && Point0.Z == Point3.Z) // in XY Plane
			{
				Point_intersection.Z = Point0.Z;
				Point_intersection.X = i_x;
				Point_intersection.Y = i_y;
			}
			else if (Point0.X == Point1.X && Point0.X == Point2.X && Point0.X == Point3.X) // in ZY Plane
			{
				Point_intersection.X = Point0.X;
				Point_intersection.Z = i_x;
				Point_intersection.Y = i_y;
			}
			else if (Point0.Y == Point1.Y && Point0.Y == Point2.Y && Point0.Y == Point3.Y) // in XZ Plane
			{
				Point_intersection.Y = Point0.Y;
				Point_intersection.X = i_x;
				Point_intersection.Z = i_y;
			}

			return 1;
		}

		public double Distance_between_points(Point3D Point0, Point3D Point1)
        {
			double distance;
			distance =Math.Sqrt(Math.Pow((Point1.X - Point0.X),2)+ Math.Pow((Point1.Y - Point0.Y), 2)+ Math.Pow((Point1.Z - Point0.Z), 2));
			
			return distance;
		}

		public Point3D MidPoint(Point3D Point0, Point3D Point1)
        {
			return new Point3D((Point0.X+ Point1.X)/2, (Point0.Y + Point1.Y) / 2, (Point0.Z + Point1.Z) / 2);
        }


		public string kamyar()
        {
            return "Kamyar_ba@yahoo.com";
		}

		//float *i_x in c --> ref float i_x
		//		void Method(ref int refArgument)
		//		{
		//			refArgument = refArgument + 44;
		//		}

		//		int number = 1;
		//		Method(ref number);
		//		Console.WriteLine(number);
		//// Output: 45


	}

	public class Point3D
	{
		public Point3D() { }
		public Point3D(double x , double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		// Properties.
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}
	}

	public class Vector3D
	{
		//Vector3D v1 = new Vector3D(2, -4, -1);
		//Vector3D v2 = new Vector3D(0, 5, 2);
		//Vector3D vout = new Vector3D().NewVectorfrom2points(P0, P1);
		//vout = Vector3D.NewVectorfrom2points(P0, P1);
		private double x, y, z;
		public Vector3D() { }
		public Vector3D(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		public static Vector3D operator -(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		public static Vector3D operator *(Vector3D lhs, double rhs)
		{
			return new Vector3D(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}
		public static Vector3D operator *(Vector3D lhs, Vector3D rhs)//cross product 
		{
			double x, y, z;
			x = lhs.y * rhs.z - lhs.z*rhs.y  ;
			y = lhs.z * rhs.x - lhs.x*rhs.z  ;
			z = lhs.x * rhs.y - lhs.y * rhs.x  ;
			return new Vector3D(x,y,z);
		}
		public Vector3D PlaneNormal(Point3D Point0, Point3D Point1, Point3D Point2)
        {
			Vector3D AB = new Vector3D().NewVectorfrom2points(Point0, Point1);
			Vector3D AC = new Vector3D().NewVectorfrom2points(Point0, Point2);
			Vector3D planeNormalVector = new Vector3D();
			planeNormalVector = AB * AC;
			return planeNormalVector;
		}
		public Vector3D NewVectorfrom2points(Point3D Point0, Point3D Point1)
        {
			return new Vector3D(Point1.X-Point0.X, Point1.Y - Point0.Y, Point1.Z - Point0.Z);
		}
		public Vector3D Normalize() //Vector3D lhs
		{
			double magnitude = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
			if (magnitude < 1e-6)
				return new Vector3D(0,0,0); 
			return new Vector3D(x/ magnitude,y/magnitude,z/magnitude);
		}

		public double magnitude()
		{
			return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
		}
		/// <summary>
		/// Output angle is in Radian
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public  double Angle(Vector3D lhs, Vector3D rhs)
		{
			return Math.Acos(lhs.Dot(rhs)/(lhs.magnitude()*rhs.magnitude()));
		}
		public double Dot(Vector3D rhs)//scalar product 
		{
			return x * rhs.x + y * rhs.y + z * rhs.z;
		}
		public double ToDegree (double Angle)
        {
			return Angle * (180 / Math.PI);
        }
		public double ToRadian(double Angle)
		{
			return Angle * (Math.PI / 180);
		}
		public bool IsBetween(Vector3D Vector1, Vector3D Vector2)
        {
			Vector3D Vector3 = new Vector3D(x, y, z);
			if ((Vector1 * Vector3).Dot(Vector1 * Vector2) >= 0 && (Vector2 * Vector3).Dot(Vector2 * Vector1) >= 0)
				return true;
			return false;
		}

		public Point3D QuadraticPlaneLineSegmentIntersection(Point3D Point_plane_0, Point3D Point_plane_1, Point3D Point_plane_2, Point3D Point_plane_3, Point3D Point_line_0, Point3D Point_line_1)
		{
			Vector3D planePoint = new Vector3D(Point_plane_0.X, Point_plane_0.Y, Point_plane_0.Z);
			Vector3D planeNormal = new Vector3D();
			planeNormal = PlaneNormal(Point_plane_0, Point_plane_1,Point_plane_2);
			planeNormal = planeNormal.Normalize();
			Vector3D linePoint = new Vector3D(Point_line_0.X, Point_line_0.Y, Point_line_0.Z);
			Vector3D lineDirection = new Vector3D();
			lineDirection= NewVectorfrom2points(Point_line_0, Point_line_1);

			Point3D intersection_point = PlaneLineIntersection(planePoint, planeNormal, linePoint, lineDirection);

			if (intersection_point == null)
				return null;
			//intersection is on the Line
			double tolerance = 1e-6;
			if ((intersection_point.X < Math.Min(Point_line_0.X, Point_line_1.X)- tolerance) ||
				(intersection_point.Y < Math.Min(Point_line_0.Y, Point_line_1.Y)- tolerance) ||
				(intersection_point.Z < Math.Min(Point_line_0.Z, Point_line_1.Z)- tolerance) ||
				(intersection_point.X > Math.Max(Point_line_0.X, Point_line_1.X)+ tolerance) ||
				(intersection_point.Y > Math.Max(Point_line_0.Y, Point_line_1.Y)+ tolerance) ||
				(intersection_point.Z > Math.Max(Point_line_0.Z, Point_line_1.Z)+ tolerance))
					return null;

			//check if the point is on the boundary of plane
			Vector3D vec1, vec2, vecx;
			bool inside_0_1_3 = false;
			vec1 = NewVectorfrom2points(Point_plane_0, Point_plane_1);
			vec2 = NewVectorfrom2points(Point_plane_0, Point_plane_3);
			vecx = NewVectorfrom2points(Point_plane_0, intersection_point);
			if (vecx.IsBetween(vec1, vec2))
				inside_0_1_3 = true;
			bool inside_2_1_3 = false;
			vec1 = NewVectorfrom2points(Point_plane_2, Point_plane_1);
			vec2 = NewVectorfrom2points(Point_plane_2, Point_plane_3);
			vecx = NewVectorfrom2points(Point_plane_2, intersection_point);
			if (vecx.IsBetween(vec1, vec2))
				inside_2_1_3 = true;
			if (inside_0_1_3 == false || inside_2_1_3 == false)
				return null;

			return intersection_point;
		}

		/// <summary>
		/// planePoint A point on the plane.
		/// planeNormal   The normal vector of the plane.
		/// linePoint     A point on the line.
		/// lineDirection The direction vector of the line.
		/// return The point of intersection between the line and the plane, null if the line is parallel to the plane.
		/// </summary>
		/// <param name="planePoint"></param>
		/// <param name="planeNormal"></param>
		/// <param name="linePoint"></param>
		/// <param name="lineDirection"></param>
		/// <returns></returns>
		private Point3D PlaneLineIntersection(Vector3D planePoint, Vector3D planeNormal, Vector3D linePoint, Vector3D lineDirection)
		{

			if (planeNormal.Dot(lineDirection.Normalize()) == 0)
			{
				return null;
			}

			double t = (planeNormal.Dot(planePoint) - planeNormal.Dot(linePoint)) / planeNormal.Dot(lineDirection.Normalize());
			Vector3D vecLinedirectionscale = lineDirection.Normalize() * t;
			Vector3D outputvector= linePoint +(vecLinedirectionscale);
			return outputvector.vectorXYZ();
		}
		public Point3D vectorXYZ()
        {
			return new Point3D(x, y, z);
		}
		public override string ToString()
		{
			return string.Format("({0:F}, {1:F}, {2:F})", x, y, z);
		}
	}
}


//Point3D P0 = new Point3D(0, 0, 0);
//Point3D P1 = new Point3D(3, 0, 0);
//Point3D P2 = new Point3D(3, 3, 0);
//Point3D P3 = new Point3D(0, 3, 0);

//Point3D P4 = new Point3D(4, 1, -1);
//Point3D P5 = new Point3D(4, 1, 5);

//Point3D P_intersection = Vector3D.QuadraticPlaneLineSegmentIntersection(P0, P1, P2, P3, P4, P5);

//if (P_intersection != null)
//	MessageBox.Show(P_intersection.ToString());
////List<Point3D> list_intersection = new List<Point3D>();

////int result=intersection.get_2Dline_intersection(P0,P1,P2,P3,ref P_intersection);
////list_intersection.Add(P_intersection);

////MessageBox.Show(intersection.Distance_between_points(P0,P1).ToString());
////MessageBox.Show($"{result}\nx={P_intersection.X} , y={P_intersection.Y} , z={P_intersection.Z}");

//Vector3D v1 = new Vector3D().NewVectorfrom2points(P0, P1);
//Vector3D v2 = new Vector3D().NewVectorfrom2points(P0, P2);
//Vector3D v3 = new Vector3D().NewVectorfrom2points(P0, P3);

//Vector3D v4 = new Vector3D().NewVectorfrom2points(P4, P2);
//Vector3D v5 = new Vector3D().NewVectorfrom2points(P4, P1);
//Vector3D v6 = new Vector3D().NewVectorfrom2points(P4, P3);

//v1 = Vector3D.PlaneNormal(P0, P1, P2);
//v1 = v1.Normalize();
//Vector3D vout = new Vector3D().NewVectorfrom2points(P0, P1);
////vout = Vector3D.NewVectorfrom2points(P0, P1);
////Vector3D.PlaneLineIntersection();

////MessageBox.Show(v3.IsBetween(v1,v2).ToString());
////MessageBox.Show(v1.ToString());