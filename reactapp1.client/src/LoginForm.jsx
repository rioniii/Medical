import React, { useState } from 'react';
import axios from 'axios'; // Make sure axios is imported

import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg';
import Header from './Header';

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isValidEmail, setIsValidEmail] = useState(true); // Add validation state
    const [isValidPassword, setIsValidPassword] = useState(true); // Add validation state
    const navigate = useNavigate();

    const GetLoginDetails = async (event) => {
        event.preventDefault(); // Prevent the default form submission
        if (!validateEmail(email) || !validatePassword(password)) {
            // Set validation states
            setIsValidEmail(validateEmail(email));
            setIsValidPassword(validatePassword(password));
            return;
        }

        try {
            const response = await axios.post('https://localhost:7107/api/Account/login', {
                email: email,
                password: password
            });

            const token = response.data.token;
            console.log("JWT Token:", token);

            const parsedToken = parseJwt(token);
            console.log("Parsed Token:", parsedToken);

            const role = parsedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            console.log("User Role:", role);

            const userId = parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            console.log("User Id:", userId);

            const name = parsedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            console.log("User Name:", name);

            cookieUtils.setUserIdInCookies(userId);
            cookieUtils.setUserRoleInCookies(role);
            cookieUtils.setTokenInCookies(token);

            const refreshToken = response.data.refreshToken;
            cookieUtils.setRefreshToken(refreshToken);

            if (role === 'Admin') {
                console.log("Navigating to admin dashboard...");
                navigate('/dashboard');
            } else {
                console.log("Navigating to home page...");
                navigate('/home');
            }

        } catch (error) {
            if (error.response) {
                console.error("Error response data:", error.response.data);
                console.error("Error response status:", error.response.status);
                console.error("Error response headers:", error.response.headers);
            } else if (error.request) {
                console.error("Error request:", error.request);
            } else {
                console.error("Error message:", error.message);
            }
            console.error("Error config:", error.config);
        }
    };

    const parseJwt = (token) => {
        try {
            const base64Url = token.split('.')[1];
            const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));

            return JSON.parse(jsonPayload);
        } catch (e) {
            console.error("Error parsing JWT token:", e);
            return null;
        }
    };

    const validateEmail = (email) => {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    };

    const validatePassword = (password) => {
        return password.length >= 8 && /[a-zA-Z]/.test(password) && /\d/.test(password);
    };

    return (
        <MDBContainer fluid className='d-flex align-items-center justify-content-center bg-image' style={{ backgroundImage: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', height: '100vh' }}>
            <div className='mask gradient-custom-3'></div>
            <MDBCard className='m-5' style={{ maxWidth: '400px', backgroundColor: 'rgba(255, 255, 255, 0.8)', borderRadius: '15px' }}>
                <MDBCardBody className='px-5 py-4'>
                    <h2 className="text-uppercase text-center mb-4">Log In</h2>
                    <MDBInput wrapperClass='mb-4' label='Your Email' size='lg' id='form2' type='email' onChange={(e) => setEmail(e.target.value)} value={email} />
                    {!isValidEmail && <div className="text-danger">Invalid email format</div>}
                    <MDBInput wrapperClass='mb-4' label='Password' size='lg' id='form3' type='password' onChange={(e) => setPassword(e.target.value)} value={password} />
                    {!isValidPassword && <div className="text-danger">Password must contain at least one letter, one number, and be at least 8 characters long</div>}
                    <div className='text-center mb-4'>
                        <NavLink to="/forgot-password" className="text-dark">Forgot Password?</NavLink>
                    </div>
                    <MDBBtn className='mb-4 w-100 gradient-custom-4' size='lg' style={{ background: 'green' }} onClick={GetLoginDetails}>Log In</MDBBtn>
                    <p className="text-center mb-0">Don't have an account? <NavLink to="/RegisterForm" className="text-dark">Register here</NavLink></p>
                </MDBCardBody>
            </MDBCard>
        </MDBContainer>
    );
}

export default Login;
