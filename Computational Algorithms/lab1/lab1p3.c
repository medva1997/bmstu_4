#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define PI 3.141592 
#define EPS 0.00001
#define N 8

struct tab
{
	float x[N];
	float y[N];
	float **z;
};

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
            	printf(" %7.2f", matrix[i][j]);
        }
        printf("\n");
    }
}

float *sort(float *choose, int n)
{
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n-i; j++)
			if (choose[j] > choose[j+1])
			{
				float tmp = choose[j];
				choose[j] = choose[j+1];
				choose[j+1] = tmp;
			}
	return choose;
}

float **sort2(float **choose, int n)
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

float F(float x, float y)
{
	return x*x+y*y/2;
}

float F2(float x, float y)
{
	return sin(x)+cos(y*y)+x*x-y*y;
}

struct tab *TableGenerate(struct tab *table, float sx, float stepx, float sy, float stepy, float(*F)(float,float))
{
	table->z = allocate_memory(N,N);
	for (int i = 0; i < N; i++)
	{
		table->x[i] = sx;
		table->y[i] = sy;
		sx += stepx;
		sy += stepy;
	}
	for (int i = 0; i < N; i++)
		for (int j = 0; j < N; j++)
			table->z[i][j] = F(table->x[i], table->y[j]);
	print_matrix(N,N,table->z);
	return table;
}

float *xTable(struct tab *table, int *istart, int nx, float X)
{
	float *xArr = malloc(sizeof(float)*(nx+1));
	int i = 0;
	while(X > table->x[i])
		i++;
	xArr[0] = table->x[i];
	int k = 1, s = 0, added = 1;
	*istart = i;
	
	while (added < nx+1)
	{
		s += pow((float)-1,k)*k;
		if (i+s >= 0 && i+s < N)
		{
			xArr[added] = table->x[i+s];
			added++;
		}
		if (i+s < *istart)
		{
			*istart = i+s;
		}
		k++;
	}
	xArr = sort(xArr,nx);
	return xArr;
}

float **yzTable(struct tab *table, int I, int ny, int Y)
{
	float **yzTab = allocate_memory(ny+1, ny+2);
	int imin = 0;
	float mindiff = fabs(Y - table->y[0]);
	for (int i = 0; i < N; i++)
		if (mindiff > fabs(Y - table->y[i]))
		{
			mindiff = fabs(Y - table->y[i]);
			imin = i;
		}
	yzTab[0][0] = table->y[imin];
	yzTab[0][1] = table->z[I][imin];
	int k = 1, s = 0, added = 1;
	while (added < ny+1)
	{
		s += pow((float)-1,k)*k;
		if (imin+s >= 0 && imin+s < N)
		{
			yzTab[added][0] = table->y[imin+s];
			yzTab[added][1] = table->z[I][imin+s];
			added++;
		}
		k++;
	}
	return yzTab;

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
	for (int i = 0; i < n; i++)
	{
		xx *=(x - choose[i][0]);
		y += xx*choose[0][k];
		k++;
	}
	return y;
}

int main(void)
{
	int nx,ny;
	float X,Y;
	printf("Степень полинома для интерполяции для Х: ");
	scanf("%d", &nx);
	printf("X = ");
	scanf("%f", &X);
	printf("Степень полинома для интерполяции для Y: ");
	scanf("%d", &ny);
	printf("Y = ");
	scanf("%f", &Y);

	struct tab *table = malloc(sizeof(struct tab));
	table = TableGenerate(table,-2,1,-2,1, F);
	//table = TableGenerate(table,-5,1.25,-5,1.25, F2);
	int istart = 0;
	float *xTab = xTable(table, &istart, nx, X);
	printf("xTab: ");
	for (int i = 0; i <= nx; i++)
	{
		printf("%f ",xTab[i]);
	}
	printf("\n");
	float **MainTab = allocate_memory(nx+1, nx+2);
	for (int i = 0; i < nx+1; i++)
	{

		MainTab[i][0] = xTab[i];
		float **yzTab = yzTable(table, istart+i, ny, Y);
		yzTab = sort2(yzTab, ny);
		yzTab = RazdRazn(yzTab, ny);
		MainTab[i][1] = Polinom(yzTab,ny, Y);
		printf("x0 = %.2f\n", xTab[i]);
		printf("_______y___F(x0,y)____________________\n");
		print_matrix(ny+1, ny+2, yzTab);
		free(yzTab);
	}

	MainTab = RazdRazn(MainTab, nx);
	printf("_______x__F(x,y)_______________________\n");
	print_matrix(nx+1, nx+2, MainTab);
	float answer = Polinom(MainTab, nx, X);
	printf("\nP(X,Y) = %.4f\n", answer);
	printf("F(X,Y) = %f\n", F(X,Y));
	free(table);
	free(xTab);
	free(MainTab);

}