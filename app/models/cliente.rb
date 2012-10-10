  class Cliente < ActiveRecord::Base
  attr_accessible :nombre
  has_many :proyectos

  validates :nombre, presence: true
end

# == Schema Information
#
# Table name: clientes
#
#  id         :integer          not null, primary key
#  nombre     :string(255)      not null
#  created_at :datetime         not null
#  updated_at :datetime         not null
#

