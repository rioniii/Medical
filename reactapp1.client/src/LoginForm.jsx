/*import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);

    const handleRegister = async (event) => {
        event.preventDefault();


        const userData = {
            email: email,
            password: password,
        };

        console.log('User Data:', userData);

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);
                if (errorData.errors && errorData.errors.Email) {
                    setEmailTakenError('Email is already taken!');
                } else {
                    setErrorMessage('Registration failed. Please try again.');
                }
                return;
            }
            const data = await response.json();
            console.log(data);

           
            
            if (role == 'admin') {
                //Linku per dashboard
            }
            if (role == 'mjek') {
                //linku per dashboard te mjekut
            }

        } catch (error) {
            console.error('Registration failed:', error);
            setErrorMessage('Registration failed. Please try again.');
        }
    };

    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <Header />
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '20px'
            }}>
                <MDBCard className='p-4' style={{
                    maxWidth: '500px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '15px',
                    boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-5 py-4'>
                        <h2 className="text-uppercase text-center mb-3">Log In</h2>
                        <form onSubmit={handleRegister}>
                            <MDBInput label='Your Email' size='lg' id='form2' type='email' value={email} onChange={handleEmailChange} />
                            <MDBInput label='Password' size='lg' id='form3' type='password' value={password} onChange={handlePasswordChange} />

                            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
                            <MDBBtn className='w-100' size='lg' type='submit'>LogIn</MDBBtn>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-3" style={{ color: 'green' }}>You don't have an account? Register here</NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;*/

/*
import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom'; // Import useNavigate for redirection
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    const navigate = useNavigate(); // Initialize useNavigate for redirecting

    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);

    const handleRegister = async (event) => {
        event.preventDefault();

        const userData = {
            email: email,
            password: password,
        };

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorText = await response.text(); // Get response text for better error handling
                console.error('Login failed:', errorText);
                setErrorMessage('Login failed. Please check your email and password.');
                return;
            }

            const data = await response.json();
            console.log('Login successful:', data);

            // Optionally, you can store the token in local storage or state
            localStorage.setItem('token', data.token); // Save the JWT token

            // Redirect to homepage after successful login
            window.location.href = '/'; // Redirect to the homepage

        } catch (error) {
            console.error('Login error:', error);
            setErrorMessage('An error occurred. Please try again later.');
        }
    };



    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <Header />
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '20px'
            }}>
                <MDBCard className='p-4' style={{
                    maxWidth: '500px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '15px',
                    boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-5 py-4'>
                        <h2 className="text-uppercase text-center mb-3">Create an account</h2>
                        <form onSubmit={handleRegister}>
                            <MDBInput label='Your Email' size='lg' id='form2' type='email' value={email} onChange={handleEmailChange} />
                            <MDBInput label='Password' size='lg' id='form3' type='password' value={password} onChange={handlePasswordChange} />

                            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
                            {successMessage && <div className="text-success mb-2">{successMessage}</div>}

                            <MDBBtn className='w-100' size='lg' type='submit'>Register</MDBBtn>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-3" style={{ color: 'green' }}>
                            Already have an account? Log in here
                        </NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;
*/



import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    const navigate = useNavigate();

    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);

    const handleLogin = async (event) => {
        event.preventDefault();

        const userData = {
            email,
            password,
        };

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Login failed:', errorText);
                setErrorMessage('Login failed. Please check your email and password.');
                return;
            }

            const data = await response.json();
            console.log('Login successful:', data);
            localStorage.setItem('token', data.token);
            navigate('/');

        } catch (error) {
            console.error('Login error:', error);
            setErrorMessage('An error occurred. Please try again later.');
        }
    };

    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <Header />
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '10px'
            }}>
                <MDBCard className='p-3' style={{
                    maxWidth: '400px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '10px',
                    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-4 py-3'>
                        <h2 className="text-uppercase text-center mb-2" style={{ fontSize: '1.3rem' }}>Log In</h2>
                        <form onSubmit={handleLogin}>
                            <div style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                                <MDBInput
                                    placeholder='Your Email'
                                    id='form1'
                                    type='email'
                                    value={email}
                                    onChange={handleEmailChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />
                                <MDBInput
                                    placeholder='Password'
                                    id='form2'
                                    type='password'
                                    value={password}
                                    onChange={handlePasswordChange}
                                    required
                                    style={{ fontSize: '0.9rem' }}
                                />

                                {errorMessage && <div className="text-danger mb-2" style={{ fontSize: '0.8rem' }}>{errorMessage}</div>}
                                {successMessage && <div className="text-success mb-2" style={{ fontSize: '0.8rem' }}>{successMessage}</div>}

                                <MDBBtn
                                    className='register-btn'
                                    size='lg'
                                    type='submit'
                                    style={{
                                        width: '120px',
                                        height: '40px',
                                        alignSelf: 'center',
                                        fontSize: '0.9rem',
                                    }}
                                >Login</MDBBtn>
                            </div>
                        </form>
                        <NavLink to="/ForgotPassword" className="text-center d-block mt-2" style={{ color: 'green', fontSize: '0.9rem' }}>
                            Forgot Password?
                        </NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Login;

