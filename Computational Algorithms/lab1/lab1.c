#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define PI 3.141592 
#define N 8
//######## ДАННЫЕ #########
float table[N][2] = {{-3, -1},
					{-2, -0.866},
					{-1, -0.5},
					{ 0,  0},
					{ 1,  0.5},
					{ 2,  0.866},
					{ 3,  1},
					{ 4,  0.866}};
int n = 3;
float x = 1.5;
//##########################

float **allocate_memory(int n, int m)
{
	float **data = malloc(n*sizeof(float*) + n*m*sizeof(float));
	if (!data)
		return NULL;
	for (int i = 0; i < n; i++)
		data[i] = (float*)((char*)data+n*sizeof(float*)+i*m*sizeof(float));
	return data;
} 

float **sort(float **choose)
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

float **NewTable(float **choose)
{
	int i = 0;
	while (table[i][0] < x)
		i++;
	choose[0] = table[i];
	int k = 1, s = 0, added = 1;
	while (added < n+1)
	{
		s += pow(-1,k)*k;
		if (i+s >= 0 && i+s < N)
		{
			choose[added] = table[i+s];
			added++;
		}
		k++;
	}
	choose = sort(choose);
	return choose;
}

float RazdRazn(int m, float **choose)
{
	float result = 0, p;
	for (int i = 0; i <= m; i++)
	{
		p = 1;
		for (int j = 0; j <= m; j++)
		{
			if ( i!=j)
				p *= choose[i][0] - choose[j][0];
		}
		result += choose[i][1]/p;
	}
	return result;
}

float Polinom(float **choose)
{
	float y = choose[0][1];
	float p = 1;
	printf("Y(%.3f) = %.3f ", x, y);
	for (int i = 1; i <= n; i++)
	{
		p *= (x - choose[i-1][0]);
		y += p*RazdRazn(i, choose);
		printf("+ (%.3f)*(%.3f) ",p, RazdRazn(i, choose));
	}
	return y;
}

float F(float x)
{
	return sin(PI*x/6);
}

int main(void)
{
	float **choose = allocate_memory(n+1, 2);
	choose = NewTable(choose);
	printf("Узлы:\n");
	for (int i= 0; i <= n; i++)
		printf("%.3f; %.3f\n", choose[i][0], choose [i][1]);
	float P = Polinom(choose);
	printf("\nПолуч. значение: Y(%.3f) = %.4f\n",x, P);
	printf("Точное значение: Y(%.3f) = %.4f\n", x, F(x));
	printf("Погрешность интерполяции: %f\n", fabs(F(x)-P));
	free(choose);
}