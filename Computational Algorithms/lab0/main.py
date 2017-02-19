from math import exp

def F(x,y):
	return exp(x**3)-exp(y)*(x**6-2*x**3*y-2*x**3+y*y+2*y+2)

def getY(x,l,r):
	#while F(x,l)*F(x,r)>0:
	#	l-=1
	#	r+=1

	while abs(F(x, (r+l)/2)) > 0.01:
		if (F(x,l)*F(x,(r+l)/2) < 0):
			r = (r+l)/2
		else: 
		#if(F(x,(r+l)/2)*F(x,r) < 0): 
			l = (r+l)/2
	return (r+l)/2

def square(l1, l2, h):
	return h*(l1+l2)/2

a = 0.0;
b = 2.0;
N = 10
h = (b-a)/N
S = 0

i = a
for i in range(N):
	S += square(getY(i,-1,1),getY(i+h,-1,1),h)
	i += h

print(S)

