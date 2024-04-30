import '../../../../style.css';

function ApplicantDetails(props){
    return (
        <div className='smallBox40 scrolling margin4 phoneBox'>
            <span className="clickRegisterText7">Applied Amount: {props.loan.amount}</span>
            <span className="clickRegisterText7">Purpose: {props.loan.purpose}</span>
            <span className="clickRegisterText7">Name: {props.loan.customers.name}</span>
            <span className="clickRegisterText7">Email: {props.loan.customers.email}</span>
            <span className="clickRegisterText7">Date of Birth: {props.loan.customers.dob}</span>
            <span className="clickRegisterText7">Phone Number: {props.loan.customers.phoneNumber}</span>
            <span className="clickRegisterText7">Aadhaar Number: {props.loan.customers.aadharNumber}</span>
            <span className="clickRegisterText7">PAN Number: {props.loan.customers.panNumber}</span>
            <span className="clickRegisterText7">Address: {props.loan.customers.address}</span>
            <span className="clickRegisterText7">Gender: {props.loan.customers.gender}</span>
            <span className="clickRegisterText7">Applied Date: {props.loan.appliedDate}</span>
        </div>
    );
}

export default ApplicantDetails;