import React, { useState, useEffect } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg'; // Ensure the image path is correct
import Header from './Header'; // Import the Header component

function Register() {
    document.title = "Register";

    // Redirect if user is already registered
    useEffect(() => {
        const user = localStorage.getItem("user");
        if (user) {
            document.location = "/";
        }
    }, []);

    const [fullName, setFullName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState(''); // New state for date of birth
    const [errorMessage, setErrorMessage] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');
    const [emailTakenError, setEmailTakenError] = useState('');

    const handleFullNameChange = (event) => setFullName(event.target.value);
    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);
    const handleConfirmPasswordChange = (event) => setConfirmPassword(event.target.value);
    const handleDateOfBirthChange = (event) => setDateOfBirth(event.target.value); // Handle date of birth change

    const handleRegister = async (event) => {
        event.preventDefault();

        if (password !== confirmPassword) {
            setPasswordMatchError('Passwords do not match');
            return;
        }

        const userData = {
            email: email,
            password: password,
            confirmPassword: confirmPassword,
            fullName: fullName, // Include full name
            dateOfBirth: dateOfBirth // Include date of birth
        };

       /* console.log('User data being sent:', userData);*/

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('Registration failed:', errorData);
                if (errorData.title === 'Email already taken') {
                    setEmailTakenError('Email is already taken!');
                } else {
                    setPasswordMatchError(errorData.title);
                }
                return;
            }

            const data = await response.json();
            console.log(data);
            /*document.location = "/login"; // Redirect to login on successful registration*/

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
                        <h2 className="text-uppercase text-center mb-3">Create an account</h2>
                        <form onSubmit={handleRegister}>
                            <MDBInput label='Your Name' size='lg' id='form1' type='text' value={fullName} onChange={handleFullNameChange} />
                            <MDBInput label='Your Email' size='lg' id='form2' type='email' value={email} onChange={handleEmailChange} />
                            <MDBInput label='Password' size='lg' id='form3' type='password' value={password} onChange={handlePasswordChange} />
                            <MDBInput label='Repeat your password' size='lg' id='form4' type='password' value={confirmPassword} onChange={handleConfirmPasswordChange} />
                            <MDBInput label='Date of Birth' size='lg' id='form5' type='date' value={dateOfBirth} onChange={handleDateOfBirthChange} />

                            {passwordMatchError && <div className="text-danger mb-2">{passwordMatchError}</div>}
                            {emailTakenError && <div className="text-danger mb-2">{emailTakenError}</div>}
                            {errorMessage && <div className="text-danger mb-2">{errorMessage}</div>}
                            <MDBBtn className='w-100' size='lg' type='submit'>Register</MDBBtn>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-3" style={{ color: 'green' }}>Already have an account? Log in here</NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;