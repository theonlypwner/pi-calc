Partial Public Class libbcmath
	Public Shared Function Add(ByRef base As BCNum, ByRef addend As BCNum) As BCNum
		'var sum, cmp_res, res_scale;

		'if (n1.n_sign === n2.n_sign) {
		'	sum = libbcmath._bc_do_add(n1, n2, scale_min);
		'	sum.n_sign = n1.n_sign;
		'} else {
		'	/* subtraction must be done. */
		'	cmp_res = libbcmath._bc_do_compare(n1, n2, false, false); /* Compare magnitudes. */
		'	switch (cmp_res) {
		'		case -1:
		'			/* n1 is less than n2, subtract n1 from n2. */
		'			sum = libbcmath._bc_do_sub(n2, n1, scale_min);
		'			sum.n_sign = n2.n_sign;
		'			break;

		'		case  0:
		'			/* They are equal! return zero with the correct scale! */
		'			res_scale = libbcmath.MAX(scale_min, libbcmath.MAX(n1.n_scale, n2.n_scale));
		'			sum = libbcmath.bc_new_num(1, res_scale);
		'			libbcmath.memset(sum.n_value, 0, 0, res_scale+1);
		'			break;

		'		case  1:
		'			/* n2 is less than n1, subtract n2 from n1. */
		'			sum = libbcmath._bc_do_sub(n1, n2, scale_min);
		'			sum.n_sign = n1.n_sign;
		'	}
		'}
		'return sum;
	End Function
End Class
