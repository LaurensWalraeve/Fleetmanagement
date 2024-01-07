import * as z from "zod";
import { UseFormReturn } from "react-hook-form";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../ui/form";
import { Input } from "../ui/input";
import { Button } from "../ui/button";
import { ScrollArea } from "../ui/scrollarea";
import { useState } from "react";
import {} from "react-hook-form";
import { DriverFields } from "./fields/driver-fields";
import { VehicleFields } from "./fields/vehicle-fields";
import { FuelCardFields } from "./fields/fuelcard-fields";
import { AddressFields } from "./fields/address-fields";

type CreateDriverFormProps = {
  form: UseFormReturn<any>;
  formSchema: z.ZodObject<any, any>;
};
const options = [
  { value: "light", label: "1" },
  { value: "dark", label: "2" },
  { value: "system", label: "3" },
];

export function CreateDriverForm({ form, formSchema }: CreateDriverFormProps) {
  const [step, setStep] = useState("Driver");

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);
  }

  return (
    <div className="h-screen flex flex-col">
      <ScrollArea className="flex-1 overflow-y-auto pb-40">
        {" "}
        {/* Ensure this is flex-1 */}
        <div className="m-1">
          {" "}
          {/* Add padding to the bottom */}
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
              {step === "Driver" && (
                <>
                  <DriverFields control={form.control} />
                  <Button
                    className="absolute bottom-20 right-0"
                    onClick={() => setStep("Address")}
                  >
                    Next
                  </Button>
                </>
              )}
              {step === "Address" && (
                <>
                  <AddressFields control={form.control} />
                  <Button
                    className="absolute bottom-20 left-0"
                    onClick={() => setStep("Driver")}
                  >
                    Previous
                  </Button>
                  <Button
                    className="absolute bottom-20 right-0"
                    onClick={() => setStep("Vehicle")}
                  >
                    Next
                  </Button>
                </>
              )}
              {step === "Vehicle" && (
                <>
                  <VehicleFields control={form.control} />
                  <Button
                    className="absolute bottom-20 left-0"
                    onClick={() => setStep("Address")}
                  >
                    Previous
                  </Button>
                  <Button
                    className="absolute bottom-20 right-0"
                    onClick={() => setStep("Fuel")}
                  >
                    Next
                  </Button>
                </>
              )}

              {step === "Fuel" && (
                <>
                  <FuelCardFields control={form.control} />
                  <Button
                    className="absolute bottom-20 left-0"
                    onClick={() => setStep("Vehicle")}
                  >
                    Previous
                  </Button>
                  <Button
                    className="absolute bottom-20 right-0"
                    onClick={() => setStep("Check")}
                  >
                    Next
                  </Button>
                </>
              )}
              {step === "Check" && (
                <>
                  {/* Voeg hier formulierelementen voor de "Check" stap toe */}
                  <DriverFields control={form.control} />
                  <VehicleFields control={form.control} />
                  <FuelCardFields control={form.control} />
                  <Button
                    className="absolute bottom-20 left-0"
                    onClick={() => setStep("Fuel")}
                  >
                    Previous
                  </Button>
                  <Button className="absolute bottom-20 right-0" type="submit">
                    Submit
                  </Button>
                </>
              )}
            </form>
          </Form>
        </div>
      </ScrollArea>
    </div>
  );
}

// import * as z from "zod";
// import { UseFormReturn } from "react-hook-form";
// import {
//   Form,
//   FormControl,
//   FormField,
//   FormItem,
//   FormLabel,
//   FormMessage,
// } from "../ui/form";
// import { Input } from "../ui/input";
// import { Button } from "../ui/button";
// import { ScrollArea } from "../ui/scrollarea";
// import { useState } from "react";
// import {} from "react-hook-form";
// import { DriverFields } from "./fields/driver-fields";
// import { VehicleFields } from "./fields/vehicle-fields";
// import { FuelCardFields } from "./fields/fuelcard-fields";
// import {
//   Select,
//   SelectContent,
//   SelectItem,
//   SelectTrigger,
//   SelectValue,
// } from "../ui/select";
// import { AddressFields } from "./fields/address-fields";

// type CreateDriverFormProps = {
//   form: UseFormReturn<any>;
//   formSchema: z.ZodObject<any, any>;
// };
// const options = [
//   { value: "light", label: "1" },
//   { value: "dark", label: "2" },
//   { value: "system", label: "3" },
// ];

// export function CreateDriverForm({ form, formSchema }: CreateDriverFormProps) {
//   const [step, setStep] = useState("Driver");

//   function onSubmit(values: z.infer<typeof formSchema>) {
//     console.log(values);
//   }

//   return (
//     <div className="h-screen flex flex-col">
//       <ScrollArea className="flex-auto">
//         <div className="m-1">
//           <Form {...form}>
//             <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
//               {/* Include your form fields here */}
//               {step === "Driver" && <DriverFields control={form.control} />}
//               {step === "Address" && <AddressFields control={form.control} />}
//               {step === "Vehicle" && <VehicleFields control={form.control} />}
//               {step === "Fuel" && <FuelCardFields control={form.control} />}
//               {step === "Check" && (
//                 <>
//                   <DriverFields control={form.control} />
//                   <VehicleFields control={form.control} />
//                   <FuelCardFields control={form.control} />
//                 </>
//               )}
//             </form>
//           </Form>
//         </div>
//       </ScrollArea>
//       <div className="flex justify-between p-4">
//         {step !== "Driver" && (
//           <Button onClick={() => setStep("previous-step")}>Previous</Button>
//         )}
//         {step !== "Check" && (
//           <Button onClick={() => setStep("next-step")}>Next</Button>
//         )}
//         {step === "Check" && (
//           <Button type="submit" form="your-form-id">
//             Submit
//           </Button>
//         )}
//       </div>
//     </div>
//   );
// }
