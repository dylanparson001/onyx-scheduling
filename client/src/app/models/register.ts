// Email = model.Email,
//   SecurityStamp = Guid.NewGuid().ToString(),
//   UserName = model.Username,
//   FirstName = model.FirstName,
//   LastName = model.LastName,
//   Address = model.Address,
//   City = model.City,
//   Role = model.Role,
//   Phone = model.Phone,
//   State = model.State

export interface Register{
  email: string,
  username: string,
  firstName: string,
  lastName: string,
  address: string,
  city: string,
  role: string,
  phone: string,
  state: string,
  password: string

}
