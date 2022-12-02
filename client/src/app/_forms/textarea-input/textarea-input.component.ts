import { Component, Input, OnInit, Self } from '@angular/core';
import {
  ControlValueAccessor,
  NgControl,
  UntypedFormControl,
} from '@angular/forms';

@Component({
  selector: 'app-textarea-input',
  templateUrl: './textarea-input.component.html',
  styleUrls: ['./textarea-input.component.css'],
})
export class TextareaInputComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() type = 'text';

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}

  //casting
  get control(): UntypedFormControl {
    return this.ngControl.control as UntypedFormControl;
  }
}
