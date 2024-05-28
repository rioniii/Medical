import React, { useState } from 'react';
import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg';

function RegisterForm() {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [repeatPassword, setRepeatPassword] = useState('');
    const [contactNumber, setContactNumber] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');
    const [emailTakenError, setEmailTakenError] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (password !== repeatPassword) {
            setPasswordMatchError("Passwords do not match!");
            return;
        }

        const userData = {
            id: 0,
            name: name,
            email: email,
            password: password,
            confirmPassword: repeatPassword,
            numriKontaktues: contactNumber
        };

        try {
            const response = await fetch('https://localhost:7107/api/User', {
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
            // Redirect or handle login status here
        } catch (error) {
            console.error('Registration failed:', error);
            setPasswordMatchError('Registration failed. Please try again.');
        }
    };

    return (
        <div className="register-form-container" style={{ backgroundImage: `url(${contactImage})`, backgroundColor: 'rgba(255, 255, 255, 0.5)', backgroundSize: 'cover', backgroundPosition: 'center', minHeight: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
            <MDBCard className='p-4' style={{ maxWidth: '500px', width: '100%', backgroundColor: 'rgba(255, 255, 255, 0.9)', borderRadius: '15px', boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)' }}>
                <MDBCardBody className='px-5 py-4'>
                    <h2 className="text-uppercase text-center mb-3">Create an account</h2>
                    <form onSubmit={handleSubmit}>
                        <MDBInput label='Your Name' size='lg' id='form1' type='text' value={name} onChange={(e) => setName(e.target.value)} />
                        <MDBInput label='Your Email' size='lg' id='form2' type='email' value={email} onChange={(e) => setEmail(e.target.value)} />
                        <MDBInput label='Password' size='lg' id='form3' type='password' value={password} onChange={(e) => setPassword(e.target.value)} pattern="^(?=.*[A-Za-z]).{8,}$" title="Password must contain at least one letter and be at least 8 characters long" />
                        <MDBInput label='Repeat your password' size='lg' id='form4' type='password' value={repeatPassword} onChange={(e) => setRepeatPassword(e.target.value)} />
                        <MDBInput label='Contact Number' size='lg' id='form5' type='text' value={contactNumber} onChange={(e) => setContactNumber(e.target.value)} pattern="\d{3}-\d{3}-\d{3}" title="Please enter the contact number in the format: 044-xxx-xxx" />
                        {passwordMatchError && <div className="text-danger mb-2">{passwordMatchError}</div>}
                        {emailTakenError && <div className="text-danger mb-2">{emailTakenError}</div>}
                        <MDBBtn className='w-100' size='lg' type='submit'>Register</MDBBtn>
                    </form>
                    <NavLink to="/LoginForm" className="text-center d-block mt-3" style={{ color: 'green' }}>Already have an account? Log in here</NavLink>
                </MDBCardBody>
            </MDBCard>
        </div>
    );
}

export default RegisterForm;
