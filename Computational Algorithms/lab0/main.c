#include <stdio.h>
#include <math.h>

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
	//0.01 - формулу относительной точности бы
	while (fabs(F(x, (r+l)/2)) > 0.01)
	{
		if (F(x,l)*F(x,(r+l)/2) < 0)
			r = (r+l)/2;
		else if(F(x,(r+l)/2)*F(x,r) < 0) 
			l = (r+l)/2;
	}

	return (r+l)/2;
}

float square(float l1, float l2, float h)
{
	return h*(l1+l2)/2;
}

int main(void)
{
	
	float a = 0.0, b = 2.0;
	int N = 10; // количество разбиений
	float h = (b-a)/N;
	float S = 0;
	
	for (float i = a; i < b; i+=h)
		S += fabs(square(gety(i,-1,1),gety(i+h,-1,1),h));

	printf("S=%f\n",S);
	
}

