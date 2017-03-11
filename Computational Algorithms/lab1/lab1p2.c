#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define PI 3.141592 
#define EPS 0.0000001

float **allocate_memory(int n, int m)
{
	float **data = (float**)calloc(n*sizeof(float*) + n*m*sizeof(float),1);
	if (!data)
		return NULL;
	for (int i = 0; i < n; i++)
		data[i] = (float*)((char*)data+n*sizeof(float*)+i*m*sizeof(float));
	for (int i = 0; i < n; i++)
		for (int j = 0; j < m; j++)
			data[i][j] = 0;
	return data;
} 

void print_matrix(int n, int m, float **matrix)
{
	for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
        	if (matrix[i][j] - (int)matrix[i][j] == 0)
        		printf("%8d", (int)matrix[i][j]);
        	else
            	printf(" %7.4f", matrix[i][j]);
        }
        printf("\n");
    }
}

float **sort(float **choose, int n)
{
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n-i; j++)
			if (choose[j][0] > choose[j+1][0])
			{
				float *tmp = choose[j];
				choose[j] = choose[j+1];
				choose[j+1] = tmp;
			}
	return choose;
}

float F1(float x, float y)
{
	return exp(pow(x,3))-exp(y)*(pow(x,6)-2*pow(x,3)*y-2*pow(x,3)+y*y+2*y+2);
}

float F2(float x, float y)
{
	return x*x*exp(-y)+y*exp(-y)-exp(x*x)*log(x*x+y);
}

float gety(float x, float l, float r, float (*F)(float,float))
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

float **generate(float **begtab, float t, float a, float b, float (*F)(float,float))
{
  float h = (b-a)/t;
  for (int i = 0; i < t; i++)
  {
    begtab[i][0] = a;
    begtab[i][1] = gety(a,-3,3, F);
    a+=h;
  }
  return begtab;
}

float **diff_table(float **new,float **t1, float **t2, int n)
{
	for (int i = 0; i < n; i++)
	{
		new[i][1] = t1[i][0];
		new[i][0] = t2[i][1]-t1[i][1];
	}
	return new;
}

float **NewTable(float x, float **table, float **choose, int n, int count)
{
	int i = 0;
	while (x > table[i][0])
		i++;

	choose[0][0] = table[i][0];
	choose[0][1] = table[i][1];
	int k = 1, s = 0, added = 1;
	while (added < n+1)
	{
		s += pow((float)-1,k)*k;
		if (i+s >= 0 && i+s < count)
		{
			choose[added][0] = table[i+s][0];
			choose[added][1] = table[i+s][1];
			added++;
		}
		k++;
	}
	return choose;
}

float **RazdRazn(float **choose, int n)
{
	int k = 0;
	for (int i =2; i < n+2; i++)
	{
		k++;
		for (int j = 0; j < n+2-i; j++)
		{
			choose[j][i] = (choose[j][i-1] - choose[j+1][i-1])/(choose[j][0] - choose[j+k][0]);
		}
	}
	return choose;
}

float Polinom(float **choose, int n, float x)
{
	float y = choose[0][1];
	int k = 2;
	float xx = 1;
	printf("\nY = %.3f", y);
	for (int i = 0; i < n; i++)
	{
		xx *=(x - choose[i][0]);
		y += xx*choose[0][k];
		printf(" + (%.3f)*(%.3f)",xx, choose[0][k]);
		k++;
	}
	return y;
}

int main(void)
{
	float a = 0.0, b = 1.3;
	float h = 0.1;
	float t = (b-a+EPS)/h;

	float **tab1 = allocate_memory(t,2);
	float **tab2 = allocate_memory(t,2);

	tab1 = generate(tab1,t,a,b, F1);
	printf("-----TABLE 1-----\n");
	print_matrix(t,2,tab1);

	tab2 = generate(tab2,t,a,b,F2);
	printf("-----TABLE 2-----\n");
	print_matrix(t,2,tab2);

	float **newtab = allocate_memory(t,2);
	newtab = diff_table(newtab,tab1,tab2, t);
	newtab = sort(newtab,t-1);
	printf("--RESULT( x<->y )--\n");
	print_matrix(t,2,newtab);

	float n = 4;//степень интерполяции
	float x = 0.0;
	float **tabRazRazn = allocate_memory(n+1,n+2);
	tabRazRazn = NewTable(x,newtab,tabRazRazn,n,t+1);
	printf("\n");
	tabRazRazn = sort(tabRazRazn,n);
	tabRazRazn = RazdRazn(tabRazRazn,n);
	print_matrix(n+1,n+2,tabRazRazn);

	x = Polinom(tabRazRazn,n,x);
	//printf("\n\nX=%f\n",x);
	float y1 = gety(x,-2,2,F1);
	//printf("y1=%f\n",y1);
	float y2 = gety(x,-2,2,F2);
	//printf("y2=%f\n",y2);

	printf("\nANSWER:\nX = %f\nY = %f\n",x,y1+(y2-y1)/2);
}