import { useState, useEffect } from "react";
import { getUserRequest } from "../../api/services/userServices";
import { handleApi } from "../../api/handleApi";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

function User() {
  const [user, setUser] = useState([]);

  useEffect(() => {
    const fetchDataAsync = async () => {
      let result = await handleApi(getUserRequest());
      setUser(result.data);
    };
    fetchDataAsync();
  }, []);
  console.log(user);
  return (
    <div id="user-management">
      <DataTable value={user} responsiveLayout="scroll">
        <Column field="userName" header="Username"></Column>
        <Column field="dob" header="Date of birth"></Column>
        <Column field="firstName" header="FirstName"></Column>
        <Column field="email" header="Email"></Column>
      </DataTable>
    </div>
  );
}

export default User;
