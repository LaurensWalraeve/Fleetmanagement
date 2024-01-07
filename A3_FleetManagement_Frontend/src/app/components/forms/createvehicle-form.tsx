import * as z from "zod";
import { UseFormReturn } from "react-hook-form";
import { Form } from "../ui/form";
import { Input } from "../ui/input";
import { Button } from "../ui/button";
import { ScrollArea } from "../ui/scrollarea";
import { useState } from "react";
import {} from "react-hook-form";
import { VehicleFields } from "./fields/vehicle-fields";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../ui/select";

type CreateDriverFormProps = {
  form: UseFormReturn<any>;
  formSchema: z.ZodObject<any, any>;
};

export function CreateVehicleForm({ form, formSchema }: CreateDriverFormProps) {
  const [step, setStep] = useState("Vehicle");

  function onSubmit(values: z.infer<typeof formSchema>) {
    console.log(values);
  }

  return (
    <ScrollArea className="h-screen pr-7">
      <div className="m-1">
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex-auto space-y-8 pb-20"
          >
            {step === "Vehicle" && (
              <>
                <VehicleFields control={form.control} />
                <Button onClick={() => setStep("Check")}>Next</Button>
              </>
            )}

            {step === "Check" && (
              <>
                <VehicleFields control={form.control} />
                <div>
                  <div className="pt-3">
                    Add existing Driver
                    <Select>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Driver" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="light">1</SelectItem>
                        <SelectItem value="dark">2</SelectItem>
                        <SelectItem value="system">3</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
                <div>
                  <div className="pt-3">
                    Add existing Fuel Card
                    <Select>
                      <SelectTrigger className="w-full">
                        <SelectValue placeholder="Fuel Card" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="light">1</SelectItem>
                        <SelectItem value="dark">2</SelectItem>
                        <SelectItem value="system">3</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
                <Button onClick={() => setStep("Vehicle")} className="mr-[10%]">
                  Previous
                </Button>
                <Button type="submit">Submit</Button>
              </>
            )}
          </form>
        </Form>
      </div>
    </ScrollArea>
  );
}
