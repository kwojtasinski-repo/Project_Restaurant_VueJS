<template>
  <div v-if="type==='text'" class="form-group">
      <label class="d-flex" :for="getAttributeConnectingInputAndLabel()">{{ label }}</label>
      <input 
          :id="getAttributeConnectingInputAndLabel()"
          :type = "type"
          :class = "error && showError ? 'form-control is-invalid' : 'form-control'"
          v-model="newValue"
          @change="valueChanged" />
      <div v-if="error && showError" class="invalid-feedback d-flex text-start">
          {{error}}
      </div>
  </div>
  <div v-else-if="type==='textarea'" class="form-group">
      <label class="d-flex" :for="getAttributeConnectingInputAndLabel()">{{label}}</label>
      <textarea
          :id="getAttributeConnectingInputAndLabel()"
          :type = "type"
          :class = "error && showError ? 'form-control is-invalid' : 'form-control'"
          v-model="newValue"
          @change="valueChanged"></textarea>
      <div v-if="error && showError" className="invalid-feedback d-flex text-start">
          {{error}}
      </div>
  </div>
  <div v-else-if="type==='select'" className="form-group">
      <label class="d-flex" :for="getAttributeConnectingInputAndLabel()">{{label}}</label>
      <select 
          :id="getAttributeConnectingInputAndLabel()"
          @change="valueChanged"
          v-model="newValue"
          :class = "error && showError ? 'form-control is-invalid' : 'form-control'">
              <option v-for="option in options" :value=option.value :key=option.value>{{option.label}}</option>
      </select>
      <div v-if="error && showError" class="invalid-feedback d-flex text-start">
          {{error}}
      </div>
  </div>
  <div v-else-if="type==='number'" className="form-group">
      <label class="d-flex" :for="getAttributeConnectingInputAndLabel()">{{ label }}</label>
      <input 
          :id="getAttributeConnectingInputAndLabel()"
          :type = "type"
          :class = "error && showError ? 'form-control is-invalid' : 'form-control'"
          v-model="newValue" 
          :step = "step"
          @change="valueChanged"
          @keypress="isNumber($event)" />
      <div v-if="error && showError" className="invalid-feedback d-flex text-start">
          {{error}}
      </div>
  </div>
  <div v-else className="form-group">
      <label class="d-flex" :for="getAttributeConnectingInputAndLabel()">{{ label }}</label>
      <input 
          :id="getAttributeConnectingInputAndLabel()"
          :type = "type"
          :class = "error && showError ? 'form-control is-invalid' : 'form-control'"
          v-model="newValue"
          @change="valueChanged" />
      <div v-if="error && showError" className="invalid-feedback d-flex text-start">
          {{error}}
      </div>
  </div>
</template>

<script>

    export default {
      name: 'InputComponent',
      props: ['type', 'value', 'label', 'options', 'step', 'error', 'showError'],
      components: {
      },
      methods: {
        valueChanged(event) {
          this.$emit('valueChanged', event.target.value);
        },
        getAttributeConnectingInputAndLabel() {
          let labelModified = this.removeWhiteSpaces(this.label);
          labelModified = this.replaceSpacesWithUnderscore(labelModified);
          const htmlFor = this.replacePolishCharacters(labelModified) + '-input';
          return htmlFor;
        },
        removeWhiteSpaces(text) {
          return text.replace(/^\s+|\s+$|\s+(?=\s)/g, "");
        },
        replaceSpacesWithUnderscore(text) {
          return text.split(' ').join('_');
        },
        replacePolishCharacters(text) {
          if ((!text || text.length === 0 )) {
              return text;
          }

          let textModified = text.replace('ą', 'a');
          textModified = textModified.replace('ć', 'c');
          textModified = textModified.replace('ę', 'e');
          textModified = textModified.replace('ł', 'l');
          textModified = textModified.replace('ń', 'n');
          textModified = textModified.replace('ó', 'o');
          textModified = textModified.replace('ś', 's');
          textModified = textModified.replace('ż', 'z');
          textModified = textModified.replace('ź', 'z');
          textModified = textModified.replace('Ą', 'A');
          textModified = textModified.replace('Ć', 'C');
          textModified = textModified.replace('Ę', 'E');
          textModified = textModified.replace('Ł', 'L');
          textModified = textModified.replace('Ń', 'N');
          textModified = textModified.replace('Ó', 'O');
          textModified = textModified.replace('Ś', 'S');
          textModified = textModified.replace('Ż', 'Z');
          textModified = textModified.replace('Ź', 'Z');
          
          return textModified;
        },
        isNumber: function(evt) {
          evt = (evt) ? evt : window.event;
          var charCode = (evt.which) ? evt.which : evt.keyCode;
          if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
            evt.preventDefault();
          } else {
            return true;
          }
        }
      },
      computed: {
        newValue: {
          get(){ return this.value },
          set(v) { this.$emit('input', v) }
        }
      },
    }
</script>

<style>
</style>