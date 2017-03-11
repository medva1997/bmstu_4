#include <stdio.h>
#include <math.h>

#define EPS 0.0001

float F(float x, float y)
{
	return exp(pow(x,3))-exp(y)*(pow(x,6)-2*pow(x,3)*y-2*pow(x,3)+y*y+2*y+2);
}

float gety(float x, float l, float r)
{
	//определение отрезка
	while (F(x,l)*F(x,r)>0)
	{
		l-=1;
		r+=1;
	}

	//поиск Y при заданном X методом половинного деления
	float middle = (l+r)/2;
	while (fabs(r-l)>EPS*middle+EPS)
	{
		middle = (l+r)/2;
		if (F(x,middle)*F(x,l)<0)
			r = middle;
		else
			l = middle;
	}
	if (fabs(F(x,l))> fabs(F(x,r)))
		return r;
	return l;
}

float Trapezia(float a, float b, float n)
{
  
	float h = (b-a)/n;
	float s = (gety(a,-1,1)+gety(b,-1,1))/2;
	for (float i = a+h; i < b-h; i+=h)
	{
		s += gety(i,-1,1);
	}
	return h*s;
}

int main(void)
{
	
	float a = 0.0, b = 2.0;
	int N = 1;

	float s1 = Trapezia(a,b,N);
	N *= 2;
	float s2 = Trapezia(a,b,N);
	while (fabs(s1-s2) > EPS)
	{
		N *= 2;
		s1 = s2;
		s2 = Trapezia(a,b,N);
		printf("%f %d\n", s2, N);
	}
	printf("\nAnswer: %f\n", s2);
	
}

