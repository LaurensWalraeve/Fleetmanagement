import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";

export type LicenseType = {
  licenseTypeID: number;
  typeName: string;
  description: string;
};

export type License = {
  licenseType: LicenseType;
  startDate: string;
  endDate: string;
};

export type FuelType = {
  fuelTypeID: number;
  typeName: string;
};

export type AcceptedFuel = {
  fuelType: FuelType;
};

export type VehicleType = {
  vehicleTypeID: number;
  typeName: string;
};

export type Vehicle = {
  vehicleId: number;
  make: string;
  model: string;
  fuelType: FuelType;
  vehicleType: VehicleType;
  chassisNumber: string;
  licensePlate: string;
  color: string;
  numberOfDoors: number;
};

export type Address = {
  addressId: number;
  street: string;
  houseNumber: string;
  zipCode: string;
  city: string;
};

export type FuelCard = {
  fuelCardId: number;
  cardNumber: string;
  expirationDate: string | null;
  pinCode: string;
  blocked: boolean;
  acceptedFuels: AcceptedFuel[];
};

export type Driver = {
  driverId: number;
  lastName: string;
  firstName: string;
  birthDate: string;
  socialSecurityNumber: string;
  licenses: License[];
  address: Address;
  vehicle: Vehicle;
  fuelCard: FuelCard;
};

export type FormData = {
  driver: {
    lastName: string;
    firstName: string;
    birthDate: string;
    socialSecurityNumber: string;
    licenseType: string | null;
    address: Address | null;
    vehicle: Vehicle | null;
    fuelCard: {
      cardNumber: string;
      expirationDate: string | null;
      pinCode: string | null;
      isBlocked: boolean;
    };
  };
};

export const formSchema = z.object({
  driver: z.object({
    lastName: z.string(),
    firstName: z.string(),
    birthDate: z.string(),
    socialSecurityNumber: z.string(),
    licenseType: z.string().nullable(),
    address: z.object({
      addressId: z.number(),
      street: z.string(),
      houseNumber: z.string(),
      zipCode: z.string(),
      city: z.string(),
    }),
    vehicle: z.object({
      vehicleId: z.number(),
      make: z.string(),
      model: z.string(),
      fuelType: z.object({
        fuelTypeID: z.number(),
        typeName: z.string(),
      }),
      vehicleType: z.object({
        vehicleTypeID: z.number(),
        typeName: z.string(),
      }),
      chassisNumber: z.string(),
      licensePlate: z.string(),
      color: z.string(),
      numberOfDoors: z.number(),
    }),
    fuelCard: z.object({
      fuelCardId: z.number(),
      cardNumber: z.string(),
      expirationDate: z.string().nullable(),
      pinCode: z.string().nullable(),
      isBlocked: z.boolean(),
      acceptedFuels: z.array(
        z.object({
          fuelType: z.object({
            fuelTypeID: z.number(),
            typeName: z.string(),
          }),
        })
      ),
    }),
  }),
});

export function useFormConfig() {
  return useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      driver: {
        lastName: "",
        firstName: "",
        birthDate: "",
        socialSecurityNumber: "",
        licenseType: null,
        address: {
          addressId: 0,
          street: "",
          houseNumber: "",
          zipCode: "",
          city: "",
        },
        vehicle: {
          vehicleId: 0,
          make: "",
          model: "",
          fuelType: {
            fuelTypeID: 0,
            typeName: "",
          },
          vehicleType: {
            vehicleTypeID: 0,
            typeName: "",
          },
          chassisNumber: "",
          licensePlate: "",
          color: "",
          numberOfDoors: 0,
        },
        fuelCard: {
          fuelCardId: 0,
          cardNumber: "",
          expirationDate: null,
          pinCode: "",
          isBlocked: false,
          acceptedFuels: [],
        },
      },
    },
  });
}
