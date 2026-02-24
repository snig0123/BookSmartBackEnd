# BookSmart API Documentation

Base URLs: `https://localhost:7200` | `http://localhost:5083`

## Authentication

JWT Bearer tokens using HMAC SHA512. The token is stored in an httponly cookie named `token` and sent automatically with requests.

### Roles

| Role    | Description                          |
|---------|--------------------------------------|
| Client  | Default role assigned on registration |
| Staff   | Can manage services and appointments |
| Admin   | Can create staff accounts            |

---

## Endpoints

### User

#### `POST /User/Register`

Register a new user account. Automatically assigned the Client role.

**Auth:** None

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "password": "string"
}
```

**Responses:** `200 OK`

---

#### `GET /User/Login`

Authenticate and receive a JWT token.

**Auth:** None

**Query parameters:**

| Parameter  | Type   | Required |
|------------|--------|----------|
| `email`    | string | Yes      |
| `password` | string | Yes      |

**Responses:**

- `200 OK` — Sets an httponly cookie named `token` and returns the user profile:

```json
{
  "userId": "guid",
  "forename": "string",
  "surname": "string",
  "roles": ["string"]
}
```

- `401 Unauthorized` — Invalid credentials

---

#### `POST /User/Logout`

Clear the authentication cookie.

**Auth:** None

**Responses:** `200 OK`

---

#### `GET /User/GetUserProfile`

Get the authenticated user's profile information. The user ID is read from the JWT token.

**Auth:** Bearer token (any authenticated user)

**Responses:**

- `200 OK`

```json
{
  "userId": "guid",
  "forename": "string",
  "surname": "string",
  "roles": ["string"]
}
```

- `404 Not Found` — User does not exist

---

### Client

#### `POST /Client/Create`

Create a new client user account. Requires Staff role.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "password": "string"
}
```

**Responses:** `201 Created`

---

#### `GET /Client/GetAll`

Get all non-deleted clients.

**Auth:** Bearer token (Staff policy)

**Responses:**

- `200 OK`

```json
[
  {
    "clientId": "guid",
    "forename": "string",
    "surname": "string",
    "email": "string",
    "telephone": "string | null"
  }
]
```

---

#### `GET /Client/GetById`

Get a single client by ID.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter  | Type | Required |
|------------|------|----------|
| `clientId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "clientId": "guid",
  "forename": "string",
  "surname": "string",
  "email": "string",
  "telephone": "string | null"
}
```

---

#### `PUT /Client/Update`

Update a client's details.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter  | Type | Required |
|------------|------|----------|
| `clientId` | guid | Yes      |

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "telephone": "string | null"
}
```

**Responses:** `200 OK`

---

#### `DELETE /Client/Delete`

Soft delete a client. Sets the user as deleted.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter  | Type | Required |
|------------|------|----------|
| `clientId` | guid | Yes      |

**Responses:** `200 OK`

---

### Staff

#### `POST /Staff/Create`

Create a new staff user account. Requires Admin role.

**Auth:** Bearer token (Admin policy)

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "password": "string"
}
```

**Responses:** `200 OK`

---

### Service

#### `POST /Service/CreateService`

Create a new service for a staff member's business.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "staffUserId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer (minutes)",
  "price": "decimal",
  "capacity": "integer"
}
```

**Responses:** `200 OK`

---

#### `GET /Service/GetService`

Get a single service by ID. Only returns non-deleted services.

**Auth:** None

**Query parameters:**

| Parameter            | Type    | Required |
|----------------------|---------|----------|
| `serviceId`          | guid    | Yes      |
| `excludeUnavailable` | boolean | No (default: false) |

**Responses:**

- `200 OK`

```json
{
  "serviceId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer",
  "price": "decimal",
  "capacity": "integer",
  "active": "boolean",
  "isAvailable": "boolean"
}
```

- `404 Not Found` — Service does not exist or has been deleted

---

#### `GET /Service/GetServicesByBusiness`

Get all active (non-deleted) services for a business.

**Auth:** None

**Query parameters:**

| Parameter            | Type    | Required |
|----------------------|---------|----------|
| `businessId`         | guid    | Yes      |
| `excludeUnavailable` | boolean | No (default: false) |

**Responses:**

- `200 OK`

```json
[
  {
    "serviceId": "guid",
    "name": "string",
    "description": "string | null",
    "duration": "integer",
    "price": "decimal",
    "capacity": "integer",
    "active": "boolean",
    "isAvailable": "boolean"
  }
]
```

---

#### `PUT /Service/UpdateService`

Update an existing service.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Request body:**

```json
{
  "staffUserId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer",
  "price": "decimal",
  "capacity": "integer"
}
```

**Responses:** `200 OK`

---

#### `DELETE /Service/DeleteService`

Soft delete a service. Sets the service as deleted and inactive.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Responses:** `200 OK`

---

### Schedule

#### `POST /Schedule/CreateSchedule`

Create a new schedule slot for a staff member. Validates the user has the Staff role.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "staffUserId": "guid",
  "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
  "startTime": "TimeOnly (HH:mm:ss)",
  "endTime": "TimeOnly (HH:mm:ss)"
}
```

**Responses:** `200 OK`

---

#### `POST /Schedule/CreateBulkSchedules`

Create multiple schedule slots at once for a single staff member. All entries must belong to the same user. Validates the user has the Staff role.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
[
  {
    "staffUserId": "guid",
    "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
    "startTime": "TimeOnly (HH:mm:ss)",
    "endTime": "TimeOnly (HH:mm:ss)"
  }
]
```

**Responses:** `200 OK`

---

#### `GET /Schedule/GetSchedule`

Get a single schedule slot by ID. Only returns non-deleted schedules.

**Auth:** None

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "scheduleId": "guid",
  "userId": "guid",
  "dayOfWeek": "integer",
  "startTime": "HH:mm:ss",
  "endTime": "HH:mm:ss",
  "active": "boolean"
}
```

- `404 Not Found` — Schedule does not exist or has been deleted

---

#### `GET /Schedule/GetSchedulesByStaff`

Get all active (non-deleted) schedule slots for a staff member. Clients can use this to see a staff member's availability.

**Auth:** None

**Query parameters:**

| Parameter     | Type | Required |
|---------------|------|----------|
| `staffUserId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "scheduleId": "guid",
    "userId": "guid",
    "dayOfWeek": "integer",
    "startTime": "HH:mm:ss",
    "endTime": "HH:mm:ss",
    "active": "boolean"
  }
]
```

---

#### `PUT /Schedule/UpdateSchedule`

Update an existing schedule slot.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Request body:**

```json
{
  "staffUserId": "guid",
  "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
  "startTime": "TimeOnly (HH:mm:ss)",
  "endTime": "TimeOnly (HH:mm:ss)"
}
```

**Responses:** `200 OK`

---

#### `DELETE /Schedule/DeleteSchedule`

Soft delete a schedule slot. Sets the schedule as deleted and inactive.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Responses:** `200 OK`

---

### Schedule Override

#### `POST /ScheduleOverride/CreateScheduleOverride`

Create a date-specific override for a staff member's recurring schedule. Use this for sick days, holidays, or adjusted hours on a specific date.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "userId": "guid",
  "date": "DateOnly (yyyy-MM-dd)",
  "isAvailable": "boolean",
  "startTime": "TimeOnly (HH:mm:ss) | null",
  "endTime": "TimeOnly (HH:mm:ss) | null"
}
```

When `isAvailable` is `false`, the staff member is unavailable for the entire date (start/end times are ignored).
When `isAvailable` is `true`, `startTime` and `endTime` define the custom hours for that date.

**Responses:** `200 OK`

---

#### `GET /ScheduleOverride/GetScheduleOverride`

Get a single schedule override by ID. Only returns non-deleted overrides.

**Auth:** None

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleOverrideId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "scheduleOverrideId": "guid",
  "userId": "guid",
  "date": "yyyy-MM-dd",
  "isAvailable": "boolean",
  "startTime": "HH:mm:ss | null",
  "endTime": "HH:mm:ss | null"
}
```

- `404 Not Found` — Override does not exist or has been deleted

---

#### `GET /ScheduleOverride/GetScheduleOverridesByStaff`

Get all active (non-deleted) schedule overrides for a staff member.

**Auth:** None

**Query parameters:**

| Parameter     | Type | Required |
|---------------|------|----------|
| `staffUserId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "scheduleOverrideId": "guid",
    "userId": "guid",
    "date": "yyyy-MM-dd",
    "isAvailable": "boolean",
    "startTime": "HH:mm:ss | null",
    "endTime": "HH:mm:ss | null"
  }
]
```

---

#### `PUT /ScheduleOverride/UpdateScheduleOverride`

Update an existing schedule override.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleOverrideId` | guid | Yes      |

**Request body:**

```json
{
  "userId": "guid",
  "date": "DateOnly (yyyy-MM-dd)",
  "isAvailable": "boolean",
  "startTime": "TimeOnly (HH:mm:ss) | null",
  "endTime": "TimeOnly (HH:mm:ss) | null"
}
```

**Responses:** `200 OK`

---

#### `DELETE /ScheduleOverride/DeleteScheduleOverride`

Soft delete a schedule override.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleOverrideId` | guid | Yes      |

**Responses:** `200 OK`

---

### Service Schedule

Manages the association between services and schedules (or schedule overrides), allowing specific services to be offered during particular schedule windows.

#### `POST /ServiceSchedule/AddServiceToSchedule`

Link a service to a recurring schedule slot.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "serviceId": "guid",
  "scheduleId": "guid"
}
```

**Responses:** `200 OK`

---

#### `DELETE /ServiceSchedule/RemoveServiceFromSchedule`

Unlink a service from a recurring schedule slot (soft delete).

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `serviceId`  | guid | Yes      |
| `scheduleId` | guid | Yes      |

**Responses:** `200 OK`

---

#### `GET /ServiceSchedule/GetServicesBySchedule`

Get all active services linked to a given schedule slot.

**Auth:** None

**Query parameters:**

| Parameter            | Type    | Required |
|----------------------|---------|----------|
| `scheduleId`         | guid    | Yes      |
| `excludeUnavailable` | boolean | No (default: false) |

**Responses:**

- `200 OK`

```json
[
  {
    "serviceId": "guid",
    "name": "string",
    "description": "string | null",
    "duration": "integer",
    "price": "decimal",
    "capacity": "integer",
    "active": "boolean",
    "isAvailable": "boolean"
  }
]
```

---

#### `GET /ServiceSchedule/GetSchedulesByService`

Get all active schedule slots linked to a given service.

**Auth:** None

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "scheduleId": "guid",
    "userId": "guid",
    "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
    "startTime": "HH:mm:ss",
    "endTime": "HH:mm:ss",
    "active": "boolean"
  }
]
```

---

#### `POST /ServiceSchedule/AddServiceToScheduleOverride`

Link a service to a schedule override.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "serviceId": "guid",
  "scheduleOverrideId": "guid"
}
```

**Responses:** `200 OK`

---

#### `DELETE /ServiceSchedule/RemoveServiceFromScheduleOverride`

Unlink a service from a schedule override (soft delete).

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter            | Type | Required |
|----------------------|------|----------|
| `serviceId`          | guid | Yes      |
| `scheduleOverrideId` | guid | Yes      |

**Responses:** `200 OK`

---

#### `GET /ServiceSchedule/GetServicesByScheduleOverride`

Get all active services linked to a given schedule override.

**Auth:** None

**Query parameters:**

| Parameter            | Type    | Required |
|----------------------|---------|----------|
| `scheduleOverrideId` | guid    | Yes      |
| `excludeUnavailable` | boolean | No (default: false) |

**Responses:**

- `200 OK`

```json
[
  {
    "serviceId": "guid",
    "name": "string",
    "description": "string | null",
    "duration": "integer",
    "price": "decimal",
    "capacity": "integer",
    "active": "boolean",
    "isAvailable": "boolean"
  }
]
```

---

#### `GET /ServiceSchedule/GetScheduleOverridesByService`

Get all active schedule overrides linked to a given service.

**Auth:** None

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "scheduleOverrideId": "guid",
    "userId": "guid",
    "date": "yyyy-MM-dd",
    "isAvailable": "boolean",
    "startTime": "HH:mm:ss | null",
    "endTime": "HH:mm:ss | null"
  }
]
```

---

### Appointment

#### `POST /Appointment/Book`

Book an appointment for the authenticated user.

**Auth:** Bearer token (any authenticated user)

**Request body:**

```json
{
  "serviceId": "guid",
  "scheduleId": "guid | null",
  "scheduleOverrideId": "guid | null",
  "requestedStartTime": "datetime (ISO 8601)",
  "comment": "string | null"
}
```

**Responses:** `201 Created`

---

#### `POST /Appointment/BookForClient`

Book an appointment on behalf of a client. Requires Staff role.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "clientUserId": "guid",
  "serviceId": "guid",
  "scheduleId": "guid | null",
  "scheduleOverrideId": "guid | null",
  "requestedStartTime": "datetime (ISO 8601)",
  "comment": "string | null"
}
```

**Responses:** `201 Created`

---

#### `GET /Appointment/GetMyAppointments`

Get all appointments for the authenticated user. The user ID is read from the JWT token.

**Auth:** Bearer token (any authenticated user)

**Responses:**

- `200 OK`

```json
[
  {
    "appointmentId": "guid",
    "clientUserId": "guid",
    "staffUserId": "guid",
    "serviceId": "guid",
    "serviceName": "string",
    "scheduleId": "guid | null",
    "scheduleOverrideId": "guid | null",
    "startDateTime": "datetime (ISO 8601)",
    "endDateTime": "datetime (ISO 8601)",
    "status": "string",
    "comment": "string | null",
    "created": "datetime (ISO 8601)"
  }
]
```

---

#### `GET /Appointment/GetStaffAppointments`

Get all appointments for the authenticated staff member. The staff user ID is read from the JWT token.

**Auth:** Bearer token (Staff policy)

**Responses:**

- `200 OK`

```json
[
  {
    "appointmentId": "guid",
    "clientUserId": "guid",
    "staffUserId": "guid",
    "serviceId": "guid",
    "serviceName": "string",
    "scheduleId": "guid | null",
    "scheduleOverrideId": "guid | null",
    "startDateTime": "datetime (ISO 8601)",
    "endDateTime": "datetime (ISO 8601)",
    "status": "string",
    "comment": "string | null",
    "created": "datetime (ISO 8601)"
  }
]
```

---

#### `GET /Appointment/GetAppointmentsByStaff`

Get all appointments for a given staff member. Requires Admin role.

**Auth:** Bearer token (Admin policy)

**Query parameters:**

| Parameter     | Type | Required |
|---------------|------|----------|
| `staffUserId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "appointmentId": "guid",
    "clientUserId": "guid",
    "staffUserId": "guid",
    "serviceId": "guid",
    "serviceName": "string",
    "scheduleId": "guid | null",
    "scheduleOverrideId": "guid | null",
    "startDateTime": "datetime (ISO 8601)",
    "endDateTime": "datetime (ISO 8601)",
    "status": "string",
    "comment": "string | null",
    "created": "datetime (ISO 8601)"
  }
]
```

---

#### `PUT /Appointment/Cancel`

Cancel an appointment. The user ID is read from the JWT token to verify ownership.

**Auth:** Bearer token (any authenticated user)

**Query parameters:**

| Parameter       | Type | Required |
|-----------------|------|----------|
| `appointmentId` | guid | Yes      |

**Responses:** `200 OK`

---

#### `PUT /Appointment/UpdateStatus`

Update the status of an appointment. Requires Staff role.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter       | Type | Required |
|-----------------|------|----------|
| `appointmentId` | guid | Yes      |

**Request body:**

```json
{
  "status": "string"
}
```

**Responses:** `200 OK`