#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define PI 3.141592 
//#define N 8
//######## ÄÀÍÍÛÅ #########
/*
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
*/
//##########################

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
	choose = sort(choose ,n);
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

float Polinom(float **choose, int n, float x, float xx)
{
	float y = choose[0][1];
	int k = 2;
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

float F(float x)
{
	return sin(x);
} 

float **generate(float **begtab, int t, float a, float b)
{
  float h = (b-a)/t;
  for (int i = 0; i < t; i++)
  {
    begtab[i][0] = a;
    begtab[i][1] = F(a);
    a+=h;
  }
  return begtab;
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

int factorial(int f)
{
    if ( f == 0 ) 
        return 1;
    return(f * factorial(f - 1));
}


int main(void)
{
	printf("%f",sin(0.1));
  int n,t;
  float a,b,X;
  printf("TABLE GENERATING\n");
  printf("Borders: ");
  scanf("%f %f",&a,&b);
  printf("Count: ");
  scanf("%d",&t);
  printf("n: ");
  scanf("%d",&n);
  printf("X = ");
  scanf("%f",&X);
  float **begtab = allocate_memory(t,2);
  begtab = generate(begtab,t,a,b);
  print_matrix(t,2,begtab);
  float **table = allocate_memory(n+1,n+2);
  table = NewTable(X,begtab,table,n, t+1);
  printf("\n");
  print_matrix(n+1,n+2,table);
  table = RazdRazn(table, n);
  printf("\n");
  print_matrix(n+1, n+2, table);


  float xx = 1;
  float P = Polinom(table, n, X, xx);
  printf("\nПолуч. значение: P(%.3f) = %.4f\n",X, P);
  printf("Точное значение: Y(%.3f) = %.4f\n", X, F(X));
  printf("Y(%.3f)-P(%.3f): %f\n",X,X, fabs(F(X)-P));	

  free(table);
  free(begtab);
}
