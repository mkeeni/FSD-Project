import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { updateEmployeeID } from "../../../../employeeSlice";

function Employee(props){

    var dispatch = useDispatch();
    var navigate = useNavigate();

    function updateEmployeeId(){
        navigate("/adminMenu/viewEmployee");
        dispatch(
            updateEmployeeID(props.employee.employeeID)
        );
    }

    return (
        <div className="whiteOutlineBox7">
            <div className="whiteOutlineBoxMargin">
                <span className="clickRegisterText">Name: {props.employee.name}</span>
                <div className="smallBox23">
                    <span className="clickRegisterText">Email: {props.employee.email}</span>
                    <span className="pointer" onClick={updateEmployeeId}>
                        <div className="rightArrow2 change-my-color"></div>
                    </span>
                </div>
            </div>
        </div>
    ); 
}

export default Employee;